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
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Models.Order.Out;
using WVA_Compulink_Integration.Models.Patient;
using WVA_Compulink_Integration.Models.Prescription;
using WVA_Compulink_Integration.ViewModels;
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

        public void DetermineView(List<Prescription> prescriptions = null, string orderName = "",  string selectedView = "")
        {
            switch (selectedView)
            {
                case "CompulinkOrders":
                    // Navigate to Lab Orders View
                    SetUpLabOrdersView();
                    OrdersContentControl.DataContext = new CompulinkOrdersViewModel();
                    break;
                case "WVAOrders":
                    // Navigate to WVA Orders View         
                    SetUpWVA_OrdersView();
                    OrdersContentControl.DataContext = new WVAOrdersViewModel();
                    break;
                case "OrderCreation":

                    // Check if order exists, if it does, return it
                    Order order = OrderCreationViewModel.GetOrder(orderName);
                  
                    // Open order creation view with the order's saved data (edits the selected order)
                    if (order != null)
                    {
                        if (prescriptions.Count < 1)
                        {
                            OrdersContentControl.DataContext = new OrderCreationViewModel(order, prescriptions, orderName);
                        }
                        // Don't add a STP item to an order and dont add a compulink order to a STP wva order
                        else if (prescriptions?[0].IsShipToPatient == true)
                        {                          
                            // Make sure user can't add a STP to another order
                            MessageBox.Show("Cannot add a Ship to Patient item to an existing WVA order!", "Compulink Integration", MessageBoxButton.OK);
                            OrdersContentControl.DataContext = new CompulinkOrdersViewModel();                                                       
                        }    
                        else if (order.ShipToPatient == "Y")
                        {
                            // Make sure user can't add a STP to another order
                            MessageBox.Show("Cannot add this item to a Ship to Patient order!", "Compulink Integration", MessageBoxButton.OK);
                            OrdersContentControl.DataContext = new CompulinkOrdersViewModel();
                        }
                        else
                            OrdersContentControl.DataContext = new OrderCreationViewModel(order, prescriptions, orderName);
                    }                       
                    else
                        // Open order creation view with a clean slate (creates a new order in the db)
                        OrdersContentControl.DataContext = new OrderCreationViewModel(prescriptions, orderName);                    
                    break;
                default:
                    OrdersContentControl.DataContext = new CompulinkOrdersViewModel();
                    break;
            }         
        }     
 
        private void CompulinkOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            SetUpLabOrdersView();
            OrdersContentControl.DataContext = new CompulinkOrdersViewModel();
        }

        private void WVA_OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            SetUpWVA_OrdersView();
            OrdersContentControl.DataContext = new WVAOrdersViewModel();
        }

        private void SetUpLabOrdersView()
        {
            // Update header to show user they are in STP view
            TabLabel.Content = "Compulink Orders";

            Color blue = (Color)ColorConverter.ConvertFromString("#FF327EC3");
            SolidColorBrush blueBrush = new SolidColorBrush(blue);
            Rect_1.Fill = blueBrush;

            Color white = (Color)ColorConverter.ConvertFromString("#ffffff");
            SolidColorBrush whiteBrush = new SolidColorBrush(white);
            Rect_2.Fill = whiteBrush;
        }

        private void SetUpWVA_OrdersView()
        {
            // Update header to show user they are in STP view
            TabLabel.Content = "WVA Orders";

            Color blue = (Color)ColorConverter.ConvertFromString("#FF327EC3");
            SolidColorBrush blueBrush = new SolidColorBrush(blue);
            Rect_2.Fill = blueBrush;

            Color white = (Color)ColorConverter.ConvertFromString("#ffffff");
            SolidColorBrush whiteBrush = new SolidColorBrush(white);
            Rect_1.Fill = whiteBrush;
        }

    }
}
