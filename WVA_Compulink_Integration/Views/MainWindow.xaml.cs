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
            // Set the main data context to the Compulink orders view 
            MainContentControl.DataContext = new OrdersViewModel();

            // Spawn a loading window and change cursor to waiting cursor
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            Mouse.OverrideCursor = Cursors.Wait;

            await Task.Run(() => List_WVA_Products.LoadProducts());

            // Close loading window and change cursor back to default arrow cursor
            loadingWindow.Close();
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    
        /// <summary>
        /// Side Tab Control Buttons For Changing Views
        /// </summary>

        private void MinimizeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
   
        private void TabOrders_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.DataContext = new OrdersViewModel();
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
