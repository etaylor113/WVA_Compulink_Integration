﻿using Newtonsoft.Json;
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
using WVA_Compulink_Integration.Models.Order.Out;
using WVA_Compulink_Integration.Models.Prescription;
using WVA_Compulink_Integration.Models.Response;
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
            RefreshOrders();
        }

        private string GetShippingString(string shipID)
        {
            switch (shipID)
            {
                case "1":
                    return "Standard";
                case "D":
                    return "UPS Ground";
                case "J":
                    return "UPS 2nd Day Air";
                case "P":
                    return "UPS Next Day Air";
                default:
                    return shipID;
            }
        }

        private void SearchOrders(string searchString)
        {
            List<Order> tempList = new List<Order>();

            try
            {
                switch (SearchFilterComboBox.SelectedIndex)
                {
                    case 0:
                        tempList = ListOrders.Where(x => x.WvaStoreID.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case 1:
                        tempList = ListOrders.Where(x => x.OrderName.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case 2:
                        tempList = ListOrders.Where(x => x.CreatedDate.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case 3:
                        tempList = ListOrders.Where(x => x.ShipToPatient.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case 4:
                        tempList = ListOrders.Where(x => x.PoNumber.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case 5:
                        tempList = ListOrders.Where(x => x.OrderedBy.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case 6:
                        tempList = ListOrders.Where(x => x.Status.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                }
                
                WvaOrdersDataGrid.Items.Clear();
                foreach (Order order in tempList)
                {
                    WvaOrdersDataGrid.Items.Add(order);
                }
            }
            catch { }
        }

        //  Return this account's orders from the server 
        private List<Order> GetWVAOrders()
        {
            try
            {
                string dsn = UserData._User.DSN;
                string endpoint = $"http://{dsn}/api/order/get-orders/" + $"{UserData._User.Account}";
                string strOrders = API.Get(endpoint, out string httpStatus);

                if (strOrders == null || strOrders == "")
                    throw new NullReferenceException();

                List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(strOrders);               

                foreach (Order order in orders)
                {
                    int quantity = 0;

                    foreach (var detail in order.Items)
                    {
                        try { quantity += Convert.ToInt32(detail.Quantity); }
                        catch { continue; }                       
                    }

                    order.Quantity = quantity;
                    order.ShippingMethod = GetShippingString(order.ShippingMethod);
                }
                   

                return orders;
            }
            catch 
            {
                return null;
            }                 
        }

        private List<Order> SetUpOrdersDataGrid()
        {
            try
            {
                ListOrders = GetWVAOrders();

                if (ListOrders == null)
                    return null;

                return ListOrders;
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
                return null;
            }
        }

        private async void RefreshOrders()
        {
            // Spawn a loading window and change cursor to waiting cursor
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                List<Order> orders = await Task.Run(() => SetUpOrdersDataGrid());

                WvaOrdersDataGrid.Items.Clear();

                foreach (Order order in ListOrders)
                {
                    WvaOrdersDataGrid.Items.Add(order);
                }
            }
            catch (Exception ex)
            {
                AppError.PrintToLog(ex);
            }            
            finally
            {
                // Close loading window and change cursor back to default arrow cursor
                loadingWindow.Close();
                Mouse.OverrideCursor = Cursors.Arrow;
            }                          
        }

        private List<Order> GetSelectedOrders()
        {
            // Get selected order
            List<Order> listOrders = new List<Order>();
            IList rows = WvaOrdersDataGrid.SelectedItems;


            // Leave if no items selected
            if (rows.Count == 0)
                return null;

            for (int i = 0; i < rows.Count; i++)
                listOrders.Add((Order)rows[i]);
           
            return listOrders;
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
            RefreshOrders();
        }

        // Edit Button
        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            List<Order> order = GetSelectedOrders();

            // Leave method if they don't select an order or if order has already been submitted
            if (order == null || order[0].Status.ToLower() == "submitted")
                return;

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                    (window as MainWindow).MainContentControl.DataContext = new OrdersView(new List<Prescription>(), order[0].OrderName, "OrderCreation");
            }
        }

        // Delete Button
        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected orders in the datagrid
            List<Order> listOrders = GetSelectedOrders();

            // Leave method if they don't select an order
            if (listOrders == null)
                return;

            foreach (var order in listOrders)
                OrderCreationViewModel.DeleteOrder(order.OrderName);      
            
            RefreshOrders();
        }
      
    }
}
