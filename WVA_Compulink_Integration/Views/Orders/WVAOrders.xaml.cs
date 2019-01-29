using Newtonsoft.Json;
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
            SetUpListOrders();
        }

        private void SearchOrders(string searchString)
        {
            try
            {
                List<Order> tempList = ListOrders.Where(x => x.OrderName.ToLower().Contains(searchString.ToLower())).ToList();

                OrdersListBox.Items.Clear();
                foreach (Order order in tempList)
                {
                    OrdersListBox.Items.Add(order.OrderName);
                }
            }
            catch { }
        }

        // Asyncronously return this account's orders from the server 
        private List<Order> GetWVAOrders()
        {       
            string endpoint = $"http://localhost:56075/CompuClient/orders/get-orders/" + $"{UserData._User.Account}";
            string strOrders = API.Get(endpoint, out string httpStatus);

            if (strOrders == null)
                throw new NullReferenceException();

            return JsonConvert.DeserializeObject<List<Order>>(strOrders);          
        }

        private  void SetUpListOrders()
        {
            try
            {
                ListOrders = GetWVAOrders();
                OrdersListBox.Items.Clear();

                foreach (Order order in ListOrders)
                {
                    OrdersListBox.Items.Add($"{order.OrderName}");
                }          
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
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
            SearchOrders(SearchTextBox.Text);
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

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            // Spawn a loading window and change cursor to waiting cursor
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            Mouse.OverrideCursor = Cursors.Wait;

            SetUpListOrders();

            // Close loading window and change cursor back to default arrow cursor
            loadingWindow.Close();
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        // Submit Button
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Leave method if they don't select an order
                if (!(OrdersListBox.SelectedItem is string selectedOrder))
                    return;

                // Create the complete order object 
                OutOrderWrapper outOrderWrapper = new OutOrderWrapper()
                {
                    OutOrder = new OutOrder()
                    {
                        ApiKey = "426761f0-3e9d-4dfd-bdbf-0f35a232c285",
                        PatientOrder = OrderCreationViewModel.GetOrder(selectedOrder)
                    }
                };

                Response response = OrderCreationViewModel.CreateOrder(outOrderWrapper);
                
                if (response.Status == "SUCCESS")
                {
                    SetUpListOrders();

                    MessageWindow messageWindow = new MessageWindow("\t\tOrder created!");
                    messageWindow.Show();
                }
                else
                    throw new Exception($"Order creation failed. Error message: {response.Message}");
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }         
        }

        // Edit Button
        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Leave method if they don't select an order
            if (!(OrdersListBox.SelectedItem is string selectedOrder))
                return;

            Order order = OrderCreationViewModel.GetOrder(selectedOrder);
                      
            List<Prescription> listPrescriptions = new List<Prescription>();

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).MainContentControl.DataContext = new OrdersView(listPrescriptions, OrdersListBox.SelectedItem as string, "OrderCreation");
                    return;
                }
            }
        }

        // Delete Button
        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Leave method if they don't select an order
            if (!(OrdersListBox.SelectedItem is string selectedOrder))
                return;

            Response response = OrderCreationViewModel.DeleteOrder(selectedOrder);

            if (response.Status == "SUCCESS")
            {
                SetUpListOrders();
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
