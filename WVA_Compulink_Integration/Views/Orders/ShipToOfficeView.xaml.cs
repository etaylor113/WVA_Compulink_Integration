using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using WVA_Compulink_Integration.MatchFinder;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Models.Order;
using WVA_Compulink_Integration.Models.Patient;
using WVA_Compulink_Integration.Models.Prescription;
using WVA_Compulink_Integration.Models.Product;
using WVA_Compulink_Integration.Models.Product.ProductIn;
using WVA_Compulink_Integration.Utility.UI_Tools;
using WVA_Compulink_Integration.ViewModels.Orders;

namespace WVA_Compulink_Integration.Views.Orders
{
    /// <summary>
    /// Interaction logic for ShipToOfficeView.xaml
    /// </summary>
    public partial class ShipToOfficeView : UserControl
    {
        // Keeps track of what product in DataGrid user has clicked on
        public int ClickedIndex { get; set; }
        public List<List<MatchProduct>> ListMatchedProducts = new List<List<MatchProduct>>();
        public List<string> ListWVA_Products = new List<string>();

        public ShipToOfficeView()
        {
            InitializeComponent();
            SetUpUI();
        }

        private void SetUpUI()
        {
            SetUpShippingComboBox();
            SetUpSTO_DataGrid();
            SetContextMenuItems();
        }

        private void SetContextMenuItems()
        {
            // Reset list of matched products 
            ListMatchedProducts.Clear();

            // Get WVA products   
            ProductIn listProductsObject = JsonConvert.DeserializeObject<ProductIn>(WVA_Products.GetProducts());

            // Get product names in DataGrid
            List<Product> compulinkProducts = new List<Product>();
            for (int i=0; i<STO_DataGrid.Items.Count; i++)
            {
                Prescription prescription = (Prescription)STO_DataGrid.Items[i];
                compulinkProducts.Add(new Product() { Description = prescription.Product });
            }

            // Loop through product names list and pass each one into matcher algorithm 
            int index = 0;
            foreach (Product product in compulinkProducts)
            {
                List<MatchProduct> matchProducts = DescriptionMatcher.FindMatch(product.Description, listProductsObject.Products, 80);
                ListMatchedProducts.Add(matchProducts);
                ShipToOfficeViewModel.ListPrescriptions[index].ProductCode = matchProducts[0].ProductKey;
                index++;
            }
        }

        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {                        
            var item = sender as MenuItem;
            string product = item.Header.ToString();

            IList rows = STO_DataGrid.SelectedItems;
            Prescription prescription = (Prescription)rows[0];

            int row = STO_DataGrid.Items.IndexOf(STO_DataGrid.CurrentItem);
            int column = STO_DataGrid.CurrentColumn.DisplayIndex;

            // Only want to change 'Products' column 
            if (column == 2)
            {
                STO_DataGrid.GetCell(row, column).Content = product;
                ShipToOfficeViewModel.ListPrescriptions[row].Product = product;
            }
        }

        private void SetUpShippingComboBox()
        {
            foreach (string shipType in ShippingTypes.ListShippingTypes)
            {
                ShippingTypeComboBox.Items.Add(shipType);              
            }
        }

        private void SetUpSTO_DataGrid()
        {
            STO_DataGrid.ItemsSource = ShipToOfficeViewModel.ListPrescriptions;          
        }

        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to clear your cart?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result.ToString() == "Yes")
            {               
                ShipToOfficeViewModel.ListPrescriptions.Clear();
                STO_DataGrid.Items.Refresh();
            }
        }

        private void STO_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Reset the products ContextMenu
            STP_ContextMenu.Items.Clear();

            // Sets 'ClickedIndex' to the index of selected cell
            IList rows = STO_DataGrid.SelectedItems;
            Prescription prescription = (Prescription)rows[0];
            ClickedIndex = STO_DataGrid.Items.IndexOf(STO_DataGrid.CurrentItem);

            foreach (MatchProduct match in ListMatchedProducts[ClickedIndex])
            {
                MenuItem menuItem = new MenuItem() { Header = match.Name };
                menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                STP_ContextMenu.Items.Add(menuItem);
            }
        }
    }
}
