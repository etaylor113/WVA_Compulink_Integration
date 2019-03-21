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
using WVA_Compulink_Integration.Models.Product.ProductOut;
using WVA_Compulink_Integration.Updates;

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
            catch (Exception ex)
            {
                AppError.PrintToLog(ex);
            }
        }

        private async Task<bool> LoadProductsAsync()
        {
            try
            {
                string dsn = UserData._User?.DSN ?? throw new NullReferenceException("DSN not set in MainWindow.LoadProducts().");
                string endpoint = $"http://{dsn}/api/product/";

                RequestOut request = new RequestOut()
                {
                    Request = new ProductOut()
                    {
                        Api_key = UserData._User?.ApiKey ?? throw new NullReferenceException("ApiKey not set in MainWindow.LoadProducts()."),
                        AccountID = UserData._User?.Account ?? throw new NullReferenceException("Account ID not set in MainWindow.LoadProducts().")
                    }
                };

                WvaProducts.LoadProductList(request, endpoint);
                return true;
            }
            catch (Exception ex)
            {
                AppError.PrintToLog(ex);
                return false;
            }
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
                bool ProductsLoaded = await Task.Run(() => LoadProductsAsync());

                if (!ProductsLoaded)
                {
                    // If products list did not load correctly, this error window will pop up and we leave the method, not opening the OrdersViewModel
                    new MessageWindow("WVA products not loaded! Please see error log in 'AppData\\Roaming\\WVA Compulink Integration\\ErrorLog' for more details.").Show();
                    return;
                }
                            
                MainContentControl.DataContext = new OrdersViewModel();
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
            finally
            {
                // Close loading window and change cursor back to default arrow cursor
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
            SetUpApp();
        }

        private void TabSettings_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.DataContext = new SettingsView();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Updater.RunLaucherUpdate();
        }
    }
}
