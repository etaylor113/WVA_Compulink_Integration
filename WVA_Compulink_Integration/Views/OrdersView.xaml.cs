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
            if (OrdersViewModel.SelectedView == "CompulinkOrders")
            {
                // Navigate to Lab Orders View
                SetUpLabOrdersView();
                OrdersContentControl.DataContext = new CompulinkOrdersViewModel();
            }
            else if (OrdersViewModel.SelectedView == "WVAOrders")
            {
                // Navigate to WVA Orders View         
                SetUpWVA_OrdersView();
                OrdersContentControl.DataContext = new WVAOrdersViewModel();
            }
            else
            {
                OrdersContentControl.DataContext = new CompulinkOrdersViewModel();
            }

            // Reset SelectedView string
            OrdersViewModel.SelectedView = "";
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
