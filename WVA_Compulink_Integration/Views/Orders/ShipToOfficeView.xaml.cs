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
            AddMenuItems();
            MatchPercentLabel.Content = $"Match Percent: {MinScoreAdjustSlider.Value}%";
        }

        private void SetContextMenuItems()
        {
            // Reset list of matched products 
            ListMatchedProducts.Clear();

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
                List<MatchProduct> matchProducts = DescriptionMatcher.FindMatch(product.Description, List_WVA_Products.ListProducts, Convert.ToDouble(MinScoreAdjustSlider.Value));

                if (matchProducts.Count > 0)
                {
                    ListMatchedProducts.Add(matchProducts);
                    ShipToOfficeViewModel.ListPrescriptions[index].ProductCode = matchProducts[0].ProductKey;
                }
                else
                {
                    ListMatchedProducts.Add(new List<MatchProduct> { new MatchProduct("No Matches Found", 0) });
                }
                index++;
            }
        }  
        
        private void AddMenuItems()
        {
            try
            {
                // Reset the products ContextMenu
                STO_ContextMenu.Items.Clear();

                // Sets 'ClickedIndex' to the index of selected cell
                if (STO_DataGrid.Items.IndexOf(STO_DataGrid.CurrentItem) > -1)
                {
                    ClickedIndex = STO_DataGrid.Items.IndexOf(STO_DataGrid.CurrentItem);
                }
               
                foreach (MatchProduct match in ListMatchedProducts[ClickedIndex])
                {
                    MenuItem menuItem = new MenuItem() { Header = match.Name };
                    menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                    STO_ContextMenu.Items.Add(menuItem);
                }
            }
            catch (Exception x)
            {

            }
        }

        private void EditColumn(int column, int row, string cellText)
        {          
            switch (column)
            {
                case 0:
                    ShipToOfficeViewModel.ListPrescriptions[row].Patient = cellText;
                    break;
                case 1:
                    ShipToOfficeViewModel.ListPrescriptions[row].Eye = cellText;
                    break;
                //
                // Skip image row
                //
                case 3:
                    ShipToOfficeViewModel.ListPrescriptions[row].Product = cellText;
                    break;
                case 4:
                    ShipToOfficeViewModel.ListPrescriptions[row].Quantity = Convert.ToInt32(cellText);
                    break;
                case 5:
                    ShipToOfficeViewModel.ListPrescriptions[row].BaseCurve = cellText;
                    break;
                case 6:
                    ShipToOfficeViewModel.ListPrescriptions[row].Diameter = cellText;
                    break;
                case 7:
                    ShipToOfficeViewModel.ListPrescriptions[row].Sphere = cellText;
                    break;
                case 8:
                    ShipToOfficeViewModel.ListPrescriptions[row].Cylinder = cellText;
                    break;
                case 9:
                    ShipToOfficeViewModel.ListPrescriptions[row].Axis = cellText;
                    break;
                case 10:
                    ShipToOfficeViewModel.ListPrescriptions[row].Add = cellText;
                    break;
                case 11:
                    ShipToOfficeViewModel.ListPrescriptions[row].Color = cellText;
                    break;
                default:
                    break;
            }
        }

        private void SetBubbleColor()
        {
            
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

        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedIndex = 0;
                MenuItem menuItem = sender as MenuItem;
                string product = menuItem.Header.ToString();

                // Get the selected menu item
                for (int i = 0; i < STO_ContextMenu.Items.Count; i++)
                {
                    MenuItem item = (MenuItem)STO_ContextMenu.Items[i];
                    
                    if (item.Header.ToString() == product)
                    {
                        selectedIndex = i;
                        break;
                    }                                                                 
                }

                // Get selected prescription object in data grid
                IList rows = STO_DataGrid.SelectedItems;
                Prescription prescription = (Prescription)rows[0];

                // Get selected row and column
                int row = STO_DataGrid.Items.IndexOf(STO_DataGrid.CurrentItem);
                int column = STO_DataGrid.CurrentColumn.DisplayIndex;

                // Only want to change 'Products' column 
                if (product != "No Matches Found" && column == 3)
                {
                    STO_DataGrid.GetCell(row, column).Content = product;
                    ShipToOfficeViewModel.ListPrescriptions[row].Product = product;

                    if (ListMatchedProducts[row][selectedIndex].MatchScore > 92)
                    {
                        ShipToOfficeViewModel.ListPrescriptions[row].ProductImagePath = @"C:\Users\evan\Desktop\Images\GreenBubble.png";
                    }
                    else if (ListMatchedProducts[row][selectedIndex].MatchScore > 75)
                    {
                        ShipToOfficeViewModel.ListPrescriptions[row].ProductImagePath = @"C:\Users\evan\Desktop\Images\YellowBubble.png";
                    }
                    else
                    {
                        ShipToOfficeViewModel.ListPrescriptions[row].ProductImagePath = @"C:\Users\evan\Desktop\Images\RedBubble.jpg";
                    }

                    STO_DataGrid.Items.Refresh();
                }
                else
                {
                    ShipToOfficeViewModel.ListPrescriptions[row].ProductImagePath = @"C:\Users\evan\Desktop\Images\RedBubble.jpg";
                    STO_DataGrid.Items.Refresh();
                }
            }
            catch (Exception x)
            {

            }
        }
      
        private void STO_DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    int column = STO_DataGrid.CurrentColumn.DisplayIndex;
                    int row = e.Row.GetIndex();
                    var typedText = e.EditingElement as TextBox;
                    string cellText = typedText.Text.Trim();

                    EditColumn(column, row, cellText);                   
                    SetContextMenuItems();
                    AddMenuItems();
                }
            }
            catch (Exception x)
            {

            }
        }

        private void STO_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {          
            SetContextMenuItems();
            AddMenuItems();
        }

        private void MinScoreAdjustSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (STO_ContextMenu.Items.Count > 0)
            {
                MatchPercentLabel.Content = $"Match Percent: {Convert.ToInt16(MinScoreAdjustSlider.Value)}%";
                SetContextMenuItems();
                AddMenuItems();
            }
        }
    }
}
