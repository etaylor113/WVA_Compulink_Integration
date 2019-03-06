﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WVA_Compulink_Integration.ViewModels;
using WVA_Compulink_Integration.ViewModels.Orders;
using WVA_Compulink_Integration.Views.Search;
using WVA_Compulink_Integration.Views;
using WVA_Compulink_Integration.Memory;
using System.IO;
using WVA_Compulink_Integration.Utility.File;
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Models.Product;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace WVA_Compulink_Integration.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool DidLoad { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            SetUpApp();          
        }

        private void SetUpApp()
        {          
            try
            {
                // Set app version at bottom of view
                AppVersionLabel.Content = $"Version: {FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion}";

                // Set the main data context to the Compulink orders view if their account number is set
                if (AccountNumAvailable())
                {
                    TryLoadOrderView();
                }                    
                else
                {
                    new MessageWindow("You must set your account number in the settings tab before continuing.").Show();
                    MainContentControl.DataContext = new SettingsViewModel();
                }                                                                 
            }
            catch (Exception x)
            {
               
            }
        }

        private async void LoadProducts()
        {
            DidLoad = List_WVA_Products.LoadProducts();
        }

        /// <summary>
        /// Side Tab Control Buttons For Changing Views
        /// </summary>
        /// 

        private async void TryLoadOrderView()
        {
            LoadingWindow loadingWindow = new LoadingWindow();

            try
            {                
                // Spawn a loading window and change cursor to waiting cursor               
                loadingWindow.Show();
                Mouse.OverrideCursor = Cursors.Wait;

                // Load product list into memory for match algorithm. If products to not load, Notify user.                
                await Task.Run(() => LoadProducts());

                // Close loading window and change cursor back to default arrow cursor
                loadingWindow.Close();
                Mouse.OverrideCursor = Cursors.Arrow;

                // Open a message window and notify user that products didn't load correctly
                if (!DidLoad)
                {
                    new MessageWindow("WVA products not loaded! Please see error log in 'AppData\\Roaming\\WVA Compulink Integration\\ErrorLog' for more details.").Show();
                    return;
                }
                else
                {
                    MainContentControl.DataContext = new OrdersViewModel();
                }
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
                loadingWindow.Close();
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        private void SetUserData()
        {
            // Set account number, api key, DSN to Mem user data
            try
            {
                // Create ActNum file path if not already
                if (!Directory.Exists(Paths.ActNumDir))
                    Directory.CreateDirectory(Paths.ActNumDir);
                                       
                if (!File.Exists(Paths.ActNumFile))
                {
                    var file = File.Create(Paths.ActNumFile);
                    file.Close();
                }

                UserData._User.Account = File.ReadAllText(Paths.ActNumFile).Trim();
                UserData._User.ApiKey  = File.ReadAllText(Paths.ApiKeyFile).Trim();
                UserData._User.DSN     = File.ReadAllText(Paths.DSNFile).Trim();
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
        }

        private bool AccountNumAvailable()
        {
            try
            {
                SetUserData();
                if (UserData._User?.Account != null && UserData._User.Account.Trim() != "")
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private void TabOrders_Click(object sender, RoutedEventArgs e)
        {
            if (AccountNumAvailable())
            {
                TryLoadOrderView();
            }
            else
            {
                MessageWindow messageWindow = new MessageWindow("You must set your account number in the settings tab before continuing.");
                messageWindow.Show();
            }
        }

        private void TabSettings_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.DataContext = new SettingsView();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WVAOrdersViewModel.SaveOrders();
        }      
    }
}
