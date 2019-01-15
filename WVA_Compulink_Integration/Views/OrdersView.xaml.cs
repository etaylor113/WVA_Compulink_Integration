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
using WVA_Compulink_Integration.Models.Patient;
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

        public void DetermineView()
        {           
            if (OrdersViewModel.SelectedView == "LabOrders")
            {
                // Navigate to Lab Orders View
                SetUpLabOrdersView();
                // OrdersContentControl
            }
            else if (OrdersViewModel.SelectedView == "WVA_Orders")
            {
                // Navigate to WVA Orders View         
                SetUpWVA_OrdersView();
                OrdersContentControl.DataContext = new WVA_OrderViewModel(OrdersViewModel.ListPrescriptions);
            }
            else
            {
                // Navigate to STO View with no data with listPatients data
                //OrdersContentControl.DataContext = new ShipToOfficeViewModel();
            }

            // Reset SelectedView string
            OrdersViewModel.SelectedView = "";
        }

        private void LabOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            SetUpLabOrdersView();
            //OrdersContentControl.DataContext = new WVA_OrderViewModel();
        }

        private void WVA_OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            SetUpWVA_OrdersView();
            OrdersContentControl.DataContext = new WVA_OrderViewModel();
        }

        private void SetUpLabOrdersView()
        {
            // Update header to show user they are in STP view
            TabLabel.Content = "Lab Orders";

            Color blue = (Color)ColorConverter.ConvertFromString("#FF327EC3");
            SolidColorBrush blueBrush = new SolidColorBrush(blue);
            STO_Rect.Fill = blueBrush;

            Color white = (Color)ColorConverter.ConvertFromString("#ffffff");
            SolidColorBrush whiteBrush = new SolidColorBrush(white);
            STP_Rect.Fill = whiteBrush;
        }

        private void SetUpWVA_OrdersView()
        {
            // Update header to show user they are in STP view
            TabLabel.Content = "WVA Orders";

            Color blue = (Color)ColorConverter.ConvertFromString("#FF327EC3");
            SolidColorBrush blueBrush = new SolidColorBrush(blue);
            STP_Rect.Fill = blueBrush;

            Color white = (Color)ColorConverter.ConvertFromString("#ffffff");
            SolidColorBrush whiteBrush = new SolidColorBrush(white);
            STO_Rect.Fill = whiteBrush;
        }

       

    }
}
