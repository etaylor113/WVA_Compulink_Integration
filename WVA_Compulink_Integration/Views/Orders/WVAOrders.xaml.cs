using Newtonsoft.Json;
using System;
using System.Collections;
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
using System.Windows.Threading;
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Models;
using WVA_Compulink_Integration.Models.Order.Out;
using WVA_Compulink_Integration.Models.Patient;
using WVA_Compulink_Integration.Models.Prescription;
using WVA_Compulink_Integration.ViewModels.Orders;
using WVA_Compulink_Integration.Views.Search;

namespace WVA_Compulink_Integration.Views.Orders
{
    /// <summary>
    /// Interaction logic for WVAOrders.xaml
    /// </summary>
    public partial class WVAOrders : UserControl
    {
 
        ToolTip toolTip = new ToolTip();
        List<Order> ListOrders { get; set; }
  
        public WVAOrders()
        {
            InitializeComponent();
            SetUp();
        }

        // Do any setup after loading the view
        private void SetUp()
        {
            IsVisibleChanged += new DependencyPropertyChangedEventHandler(LoginControl_IsVisibleChanged);
            SetUpOrdersDataGrid();
        }

        private void SearchOrders(string searchString)
        {
            List<Order> tempList = new List<Order>();

            try
            {
                
                
                tempList = ListOrders.Where(x => x.OrderName.ToLower().Contains(searchString.ToLower())).ToList();



                WvaOrdersDataGrid.Items.Clear();
                foreach (Order order in tempList)
                {
                    WvaOrdersDataGrid.Items.Add(order.OrderName);
                }
            }
            catch { }
        }

        //  Return this account's orders from the server 
        private List<Order> GetWVAOrders()
        {
            string dsn = UserData._User.DSN;
            string endpoint = $"http://{dsn}/api/order/get-orders/" + $"{UserData._User.Account}";
            string strOrders = API.Get(endpoint, out string httpStatus);

            if (strOrders == null)
                throw new NullReferenceException();

            return JsonConvert.DeserializeObject<List<Order>>(strOrders);          
        }

        private void SetUpOrdersDataGrid()
        {
            try
            {
                ListOrders = GetWVAOrders();
                WvaOrdersDataGrid.Items.Clear();

                foreach (Order order in ListOrders)
                {
                    WvaOrdersDataGrid.Items.Add(order);
                }          
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
        }

        private Order GetSelectedOrder()
        {
            // Get selected order
            IList rows = WvaOrdersDataGrid.SelectedItems;
            Order order = (Order)rows[0];

            return order;
        }

        // Allow SearchTextBox to get focus
        void LoginControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                Dispatcher.BeginInvoke(
                DispatcherPriority.ContextIdle,
                new Action(delegate ()
                {
                    SearchTextBox.Focus();
                }));
            }
        }

        // Search Text Box
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //SearchOrders(SearchTextBox.Text);
        }

        // Refresh Button
        private void RefreshButton_MouseEnter(object sender, MouseEventArgs e)
        {
            toolTip.Content = "Refresh Content";
            toolTip.IsOpen = true;
            RefreshImage.Source = new BitmapImage(new Uri(@"/Resources/icons8-available-updates-48.png", UriKind.Relative));
        }

        private void RefreshButton_MouseLeave(object sender, MouseEventArgs e)
        {
            toolTip.IsOpen = false;
            RefreshImage.Source = new BitmapImage(new Uri(@"/Resources/icons8-available-updates-filled-48.png", UriKind.Relative));
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            // Spawn a loading window and change cursor to waiting cursor
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            Mouse.OverrideCursor = Cursors.Wait;

            await Task.Run(() => SetUpOrdersDataGrid());

            // Close loading window and change cursor back to default arrow cursor
            loadingWindow.Close();
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        // Edit Button
        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Order order = GetSelectedOrder();

            // Leave method if they don't select an order
            if (order == null)
                return;

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).MainContentControl.DataContext = new OrdersView(new List<Prescription>(), order.OrderName, "OrderCreation");
                    return;
                }
            }
        }

        // Delete Button
        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Order order = GetSelectedOrder();

            // Leave method if they don't select an order
            if (order == null)
                return;

            Response response = OrderCreationViewModel.DeleteOrder(order.OrderName);

            if (response.Status == "SUCCESS")
            {
                SetUpOrdersDataGrid();
                MessageWindow messageWindow = new MessageWindow("\t\tOrder deleted!");
                messageWindow.Show();
            }
            else
            {
                MessageBox.Show("An error has occurred. Order not deleted.", "", MessageBoxButton.OK);
            }
        }
      
    }
}
