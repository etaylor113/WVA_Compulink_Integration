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
using WVA_Compulink_Integration.Views.Search;

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
            MainContentControl.DataContext = new SearchViewModel();
        }

        private void SetUpApp()
        {
            Width += 200;
            Height += 100;    
            // Spawn a loading window and change cursor to waiting cursor
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            Mouse.OverrideCursor = Cursors.Wait;

            LoadProductsList();

            // Close loading window and change cursor back to default arrow cursor
            loadingWindow.Close();
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private async void LoadProductsList()
        {
            List_WVA_Products.LoadProducts();
        }

        /// <summary>
        /// Side Tab Control Buttons For Changing Views
        /// </summary>

        private void MinimizeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void TabSearch_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.DataContext = new SearchViewModel();
        }

        private void TabOrders_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.DataContext = new OrdersViewModel();
        }

        private void TabSettings_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.DataContext = new SettingsLoginViewModel();
        }

        private void TabHelp_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.DataContext = new HelpViewModel();
        }

        
    }
}
