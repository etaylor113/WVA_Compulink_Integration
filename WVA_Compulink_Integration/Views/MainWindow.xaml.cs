using System;
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
using WVA_Compulink_Integration.Models.Product;
using WVA_Compulink_Integration.ViewModels;
using WVA_Compulink_Integration.ViewModels.Orders;
using WVA_Compulink_Integration.Views.Search;
using WVA_Compulink_Integration.Views;
using WVA_Compulink_Integration.Memory;
using System.IO;
using WVA_Compulink_Integration.Utility.File;
using WVA_Compulink_Integration.Error;

namespace WVA_Compulink_Integration.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUpApp();          
        }

        private async void SetUpApp()
        {
            // Set the main data context to the Compulink orders view if their account number is set
            if (AccountNumAvailable())
            {
                MainContentControl.DataContext = new OrdersViewModel();
            }
            else
            {
                MessageWindow messageWindow = new MessageWindow("You must set your account number in the settings tab before continuing.");
                messageWindow.Show();
                return;
            }
            
            // Spawn a loading window and change cursor to waiting cursor
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            Mouse.OverrideCursor = Cursors.Wait;
           
            // Load product list into memory for match algorithm
            await Task.Run(() => List_WVA_Products.LoadProducts());

            // Close loading window and change cursor back to default arrow cursor
            loadingWindow.Close();
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        /// <summary>
        /// Side Tab Control Buttons For Changing Views
        /// </summary>
        /// 

        private void SetActNum()
        {
            // Set account number, api key, DSN to Mem user data
            try
            {
                UserData._User.Account = File.ReadAllText(Paths.ActNumFile).Trim();
                UserData._User.ApiKey  = File.ReadAllText(Paths.apiKeyFile).Trim();
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
                SetActNum();
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
                MainContentControl.DataContext = new OrdersViewModel();
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

        private void TabHelp_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.DataContext = new HelpViewModel();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WVAOrdersViewModel.SaveOrders();
        }      
    }
}
