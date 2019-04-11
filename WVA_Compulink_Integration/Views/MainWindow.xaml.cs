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
using WVA_Compulink_Integration.ViewModels;
using WVA_Compulink_Integration.ViewModels.Orders;
using WVA_Compulink_Integration.Views.Search;
using WVA_Compulink_Integration.Views;
using WVA_Compulink_Integration.Memory;
using System.IO;
using WVA_Compulink_Integration.Utility.Files;
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Models.Product;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using WVA_Compulink_Integration.Models.Product.ProductOut;
using WVA_Compulink_Integration.Updates;
using WVA_Compulink_Integration.Utility.Actions;

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
                AppError.ReportOrLog(ex);
            }
        }

        private async Task<bool> LoadProductsAsync()
        {
            try
            {
                string dsn = UserData.Data?.DSN ?? throw new NullReferenceException("DSN not set in MainWindow.LoadProducts().");
                string endpoint = $"http://{dsn}/api/product/";

                RequestOut request = new RequestOut()
                {
                    Request = new ProductOut()
                    {
                        Api_key = UserData.Data?.ApiKey ?? throw new NullReferenceException("ApiKey not set in MainWindow.LoadProducts()."),
                        AccountID = UserData.Data?.Account ?? throw new NullReferenceException("Account ID not set in MainWindow.LoadProducts().")
                    }
                };

                WvaProducts.LoadProductList(request, endpoint);
                return true;
            }
            catch (Exception ex)
            {
                AppError.ReportOrLog(ex);
                return false;
            }
        }

        private void ReportActionLogData()
        {
            List<ActionData> data = ActionLogger.GetData();
            
            foreach (ActionData d in data)
            {
                bool dataReported = ActionLogger.ReportData($"FileName={d.FileName} \n{d.Content}");

                if (dataReported)
                    File.Delete(d.FileName);
            }
        }

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
                AppError.ReportOrLog(x);
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

                UserData.Data.Account = File.ReadAllText(Paths.ActNumFile).Trim();
                UserData.Data.ApiKey  = File.ReadAllText(Paths.ApiKeyFile).Trim();
                UserData.Data.DSN     = File.ReadAllText(Paths.DSNFile).Trim();
            }
            catch (Exception x)
            {
                AppError.ReportOrLog(x);
            }
        }

        private bool AccountNumAvailable()
        {
            try
            {
                SetUserData();
                if (UserData.Data?.Account != null && UserData.Data.Account.Trim() != "")
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
            string location =  "WVA_Compulink_Integration.Views.MainWindow.Window_Closing()";
            string actionMessage = "<Start_Launcher_Update>";
            ActionLogger.Log(location, actionMessage);

            Updater.RunLaucherUpdate();

            actionMessage = "<End_Launcher_Update>";
            ActionLogger.Log(location, actionMessage);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Send data collection of user activity for statistics and diagnosing user problems
            ReportActionLogData();

            string location = "WVA_Compulink_Integration.Views.MainWindow.Window_Loaded()";
            string actionMessage = "<App_Launch>";
            ActionLogger.Log(location, actionMessage);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            string location = "WVA_Compulink_Integration.Views.MainWindow.Window_Closed()";
            string actionMessage = " <App_Exit>\n";
            ActionLogger.Log(location, actionMessage);
        }
    }
}
