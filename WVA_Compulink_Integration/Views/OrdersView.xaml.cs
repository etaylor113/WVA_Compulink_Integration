using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WVA_Compulink_Integration.Models.Order.Out;
using WVA_Compulink_Integration.Models.Prescription;
using WVA_Compulink_Integration.ViewModels.Orders;

namespace WVA_Compulink_Integration.Views
{
    public partial class OrdersView : UserControl
    {
        public OrdersView()
        {
            InitializeComponent();
            DetermineView();
        }
       
        public OrdersView(List<Prescription> prescriptions, string orderName, string selectedView)
        {
            InitializeComponent();
            DetermineView(prescriptions, orderName, selectedView);
        }

        // Navigate to Views\Orders\ViewOrderDetails with 'order' object
        public OrdersView(Order order)
        {
            InitializeComponent();
            OrdersContentControl.DataContext = new ViewOrderDetailsViewModel(order);
        }

        public void DetermineView(List<Prescription> prescriptions = null, string orderName = "",  string selectedView = "")
        {
            switch (selectedView)
            {
                case "CompulinkOrders":
                    SetUpLabOrdersView();
                    OrdersContentControl.DataContext = new CompulinkOrdersViewModel();  // Navigate to Views\Orders\CompulinkOrders
                    break;
                case "WVAOrders":
                    SetUpWvaOrdersView();
                    OrdersContentControl.DataContext = new WVAOrdersViewModel();  // Navigate to Views\Orders\WvaOrders       
                    break;
                case "OrderCreation":

                    // Check if order exists, if it does, return it
                    Order order = OrderCreationViewModel.GetOrder(orderName);

                    // If (true) this order exists in the database on their server
                    // If (false) then this is a new order (i.e. doesn't exists in the database)
                    if (order != null)
                    {
                        if (prescriptions.Count < 1)
                            OrdersContentControl.DataContext = new OrderCreationViewModel(order, prescriptions, orderName);

                        // Can't add a STP item to an order and dont add a compulink order to a STP wva order. 
                        else if (prescriptions?[0].IsShipToPatient == true)
                        {                          
                            // Make sure user can't add a STP to another order
                            MessageBox.Show("Cannot add a Ship to Patient item to an existing WVA order!", "Compulink Integration", MessageBoxButton.OK);
                            GoToCompulinkOrdersView();
                        }    
                        else if (order.ShipToPatient == "Y")
                        {
                            // Make sure user can't add a STP to another order
                            MessageBox.Show("Cannot add this item to a Ship to Patient order!", "Compulink Integration", MessageBoxButton.OK);
                            GoToCompulinkOrdersView();
                        }
                        else
                            OrdersContentControl.DataContext = new OrderCreationViewModel(order, prescriptions, orderName);
                    }
                    else
                    {
                        // Open OrderCreationView with a new order (creates a new order in the db)
                        OrdersContentControl.DataContext = new OrderCreationViewModel(prescriptions, orderName);
                    }
                    break;
                default:
                    GoToCompulinkOrdersView();
                    break;
            }         
        }

        private void GoToCompulinkOrdersView()
        {
            OrdersContentControl.DataContext = new CompulinkOrdersViewModel(); 
        }

        private void SetUpLabOrdersView()
        {
            // Update header to show user they are in 'Compulink Orders' view
            TabLabel.Content = "Compulink Orders";

            Color blue = (Color)ColorConverter.ConvertFromString("#FF327EC3");
            SolidColorBrush blueBrush = new SolidColorBrush(blue);
            Rect_1.Fill = blueBrush;

            Color white = (Color)ColorConverter.ConvertFromString("#ffffff");
            SolidColorBrush whiteBrush = new SolidColorBrush(white);
            Rect_2.Fill = whiteBrush;
        }

        private void SetUpWvaOrdersView()
        {
            // Update header to show user they are in 'WVA Orders' view
            TabLabel.Content = "WVA Orders";
            Color blue = (Color)ColorConverter.ConvertFromString("#FF327EC3");
            SolidColorBrush blueBrush = new SolidColorBrush(blue);
            Rect_2.Fill = blueBrush;

            Color white = (Color)ColorConverter.ConvertFromString("#ffffff");
            SolidColorBrush whiteBrush = new SolidColorBrush(white);
            Rect_1.Fill = whiteBrush;
        }

        private void CompulinkOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            SetUpLabOrdersView();
            OrdersContentControl.DataContext = new CompulinkOrdersViewModel();
        }

        private void WVA_OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            SetUpWvaOrdersView();
            OrdersContentControl.DataContext = new WVAOrdersViewModel();
        }       

    }
}
