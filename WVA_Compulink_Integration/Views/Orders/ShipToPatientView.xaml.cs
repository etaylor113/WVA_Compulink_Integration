using Newtonsoft.Json;
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
using WVA_Compulink_Integration.MatchFinder;
using WVA_Compulink_Integration.Models.Order;
using WVA_Compulink_Integration.Models.Patient;
using WVA_Compulink_Integration.Models.Prescription;
using WVA_Compulink_Integration.Models.Product;
using WVA_Compulink_Integration.Models.Product.ProductIn;
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
        // Keeps track of what product in DataGrid user has clicked on
        public int ClickedIndex { get; set; }
        public List<List<MatchProduct>> ListMatchedProducts = new List<List<MatchProduct>>();
        public List<string> ListWVA_Products = new List<string>();

        public ShipToPatientView()
        {
            InitializeComponent();
            SetUpUI();
        }

        private void SetUpUI()
        {
            SetUpShippingComboBox();
            SetUpSTP_DataGrid();
            SetContextMenuItems();        
        }

        private void SetContextMenuItems()
        {
            // Reset list of matched products 
            ListMatchedProducts.Clear();

            // Get product names in DataGrid
            List<Product> compulinkProducts = new List<Product>();
            for (int i = 0; i < STP_DataGrid.Items.Count; i++)
            {
                Prescription prescription = (Prescription)STP_DataGrid.Items[i];
                compulinkProducts.Add(new Product() { Description = prescription.Product });
            }

            // Loop through product names list and pass each one into matcher algorithm 
            int index = 0;
            foreach (Product product in compulinkProducts)
            {
                List<MatchProduct> matchProducts = DescriptionMatcher.FindMatch(product.Description, List_WVA_Products.ListProducts, 80);

                if (matchProducts.Count > 0)
                {
                    ListMatchedProducts.Add(matchProducts);
                    ShipToPatientViewModel.ListPrescriptions[index].ProductCode = matchProducts[0].ProductKey;
                }
                else
                {
                    ListMatchedProducts.Add(new List<MatchProduct> { new MatchProduct("No Matches Found", 0) });
                }
                index++;
            }
        }

        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {           
           // ContextMenu Event
            var menuItem = sender as MenuItem;
            string product = menuItem.Header.ToString();

            if (product != "No Matches Found")
            {
                IList rows = STP_DataGrid.SelectedItems;
                Prescription prescription = (Prescription)rows[0];

                int row = STP_DataGrid.Items.IndexOf(STP_DataGrid.CurrentItem);
                int column = STP_DataGrid.CurrentColumn.DisplayIndex;

                if (column == 3)
                {
                    STP_DataGrid.GetCell(row, column).Content = product;
                    ShipToPatientViewModel.ListPrescriptions[row].Product = product;
                }
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

        private void STP_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Reset the products ContextMenu
            STP_ContextMenu.Items.Clear();

            // Sets 'ClickedIndex' to the index of selected cell
            IList rows = STP_DataGrid.SelectedItems;
            Prescription prescription = (Prescription)rows[0];
            ClickedIndex = STP_DataGrid.Items.IndexOf(STP_DataGrid.CurrentItem);

            foreach (MatchProduct match in ListMatchedProducts[ClickedIndex])
            {
                MenuItem menuItem = new MenuItem() { Header = match.Name };
                menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                STP_ContextMenu.Items.Add(menuItem);
            }
        }

        private void STP_DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    int column = STP_DataGrid.CurrentColumn.DisplayIndex;
                    int row = e.Row.GetIndex();
                    var typedText = e.EditingElement as TextBox;
                    string cellText = typedText.Text.Trim();

                    switch (column)
                    {
                        case 0:
                            ShipToPatientViewModel.ListPrescriptions[row].Patient = cellText;
                            break;
                        case 1:
                            ShipToPatientViewModel.ListPrescriptions[row].Eye = cellText;
                            break;
                        // 
                        // Skip image row
                        //
                        case 3:
                            ShipToPatientViewModel.ListPrescriptions[row].Product = cellText;
                            break;
                        case 4:
                            ShipToPatientViewModel.ListPrescriptions[row].Quantity = Convert.ToInt32(cellText);
                            break;
                        case 5:
                            ShipToPatientViewModel.ListPrescriptions[row].BaseCurve = cellText;
                            break;
                        case 6:
                            ShipToPatientViewModel.ListPrescriptions[row].Diameter = cellText;
                            break;
                        case 7:
                            ShipToPatientViewModel.ListPrescriptions[row].Sphere = cellText;
                            break;
                        case 8:
                            ShipToPatientViewModel.ListPrescriptions[row].Cylinder = cellText;
                            break;
                        case 9:
                            ShipToPatientViewModel.ListPrescriptions[row].Axis = cellText;
                            break;
                        case 10:
                            ShipToPatientViewModel.ListPrescriptions[row].Add = cellText;
                            break;
                        case 11:
                            ShipToPatientViewModel.ListPrescriptions[row].Color = cellText;
                            break;
                        default:
                            break;
                    }
                    SetContextMenuItems();
                }
            }
            catch{}
        }
    }
}
