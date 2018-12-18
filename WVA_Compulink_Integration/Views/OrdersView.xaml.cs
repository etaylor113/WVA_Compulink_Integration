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
    /// <summary>
    /// Interaction logic for OrdersView.xaml
    /// </summary>
    public partial class OrdersView : UserControl
    {
        public OrdersView()
        {
            InitializeComponent();
            DetermineView();
        }

        public void DetermineView()
        {           
            if (OrdersViewModel.SelectedView == "STO")
            {
                // Navigate to STO View with listPatients data
                SetUpShipToOfficeView();
                OrdersContentControl.DataContext = new ShipToOfficeViewModel(OrdersViewModel.ListPrescriptions);
            }
            else if (OrdersViewModel.SelectedView == "STP")
            {
                // Navigate to STP View         
                SetUpShipToPatientView();
                OrdersContentControl.DataContext = new ShipToPatientViewModel(OrdersViewModel.ListPrescriptions);
            }
            else
            {
                // Navigate to STO View with no data with listPatients data
                OrdersContentControl.DataContext = new ShipToOfficeViewModel();
            }

            // Reset SelectedView string
            OrdersViewModel.SelectedView = "";
        }

        private void ShipToOfficeButton_Click(object sender, RoutedEventArgs e)
        {
            SetUpShipToOfficeView();
            OrdersContentControl.DataContext = new ShipToOfficeViewModel();
        }

        private void ShipToPatientButton_Click(object sender, RoutedEventArgs e)
        {
            SetUpShipToPatientView();
            OrdersContentControl.DataContext = new ShipToPatientViewModel();
        }

        private void SetUpShipToOfficeView()
        {
            // Update header to show user they are in STP view
            TabLabel.Content = "Orders - S.T.O";

            Color blue = (Color)ColorConverter.ConvertFromString("#FF327EC3");
            SolidColorBrush blueBrush = new SolidColorBrush(blue);
            STO_Rect.Fill = blueBrush;

            Color white = (Color)ColorConverter.ConvertFromString("#ffffff");
            SolidColorBrush whiteBrush = new SolidColorBrush(white);
            STP_Rect.Fill = whiteBrush;
        }

        private void SetUpShipToPatientView()
        {
            // Update header to show user they are in STP view
            TabLabel.Content = "Orders - S.T.P";

            Color blue = (Color)ColorConverter.ConvertFromString("#FF327EC3");
            SolidColorBrush blueBrush = new SolidColorBrush(blue);
            STP_Rect.Fill = blueBrush;

            Color white = (Color)ColorConverter.ConvertFromString("#ffffff");
            SolidColorBrush whiteBrush = new SolidColorBrush(white);
            STO_Rect.Fill = whiteBrush;
        }

       

    }
}
