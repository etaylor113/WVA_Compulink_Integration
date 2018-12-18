using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WVA_Compulink_Integration.Models.Order;
using WVA_Compulink_Integration.Models.Patient;
using WVA_Compulink_Integration.Models.Prescription;
using WVA_Compulink_Integration.Utility.UI_Tools;
using WVA_Compulink_Integration.ViewModels;
using WVA_Compulink_Integration.ViewModels.Orders;

namespace WVA_Compulink_Integration.Views.Orders
{
    /// <summary>
    /// Interaction logic for ShipToPatientView.xaml
    /// </summary>
    public partial class ShipToPatientView : UserControl
    {
        public ShipToPatientView()
        {
            InitializeComponent();
            SetUpUI();
        }

        private void SetUpUI()
        {
            SetUpShippingComboBox();
            SetUpSTP_DataGrid();

            string[] products = new string[] { "Acuvue Oasys 12 Packs", "Biofinity Multifocal Trials", "Hydraluxe 90 Packs" };
              
            foreach (string product in products)
            {
                MenuItem menuItem = new MenuItem() { Header = product };
                         menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                STP_ContextMenu.Items.Add(menuItem);
            }                
        }

        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            

           // ContextMenu Event
            var menuItem = sender as MenuItem;
            string product = menuItem.Header.ToString();

            IList rows = STP_DataGrid.SelectedItems;
            Prescription prescription = (Prescription)rows[0];

            int row = STP_DataGrid.Items.IndexOf(STP_DataGrid.CurrentItem);
            int column = STP_DataGrid.CurrentColumn.DisplayIndex;

            if (column == 2)
            {
                STP_DataGrid.GetCell(row, column).Content = product;
            }
           
        }

        private void SetUpShippingComboBox()
        {
            foreach (string shipType in ShippingTypes.ListShippingTypes)
            {
                ShippingTypeComboBox.Items.Add(shipType);
            }
        }

        private void SetUpSTP_DataGrid()
        {
            STP_DataGrid.ItemsSource = ShipToPatientViewModel.ListPrescriptions;
        }

        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to clear your cart?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result.ToString() == "Yes")
            {
                ShipToPatientViewModel.ListPrescriptions.Clear();
                STP_DataGrid.Items.Refresh();
            }
        }


       

       


    }
}
