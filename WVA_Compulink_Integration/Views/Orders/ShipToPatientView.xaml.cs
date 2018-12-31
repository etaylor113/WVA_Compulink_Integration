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
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.MatchFinder;
using WVA_Compulink_Integration.Models.Order;
using WVA_Compulink_Integration.Models.Order.Out;
using WVA_Compulink_Integration.Models.Patient;
using WVA_Compulink_Integration.Models.Prescription;
using WVA_Compulink_Integration.Models.Product;
using WVA_Compulink_Integration.Models.Product.ProductIn;
using WVA_Compulink_Integration.Models.ProductParameters;
using WVA_Compulink_Integration.Models.Validations;
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
            AddMenuItems();
            MatchPercentLabel.Content = $"Match Percent: {MinScoreAdjustSlider.Value}%";
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
                List<MatchProduct> matchProducts = DescriptionMatcher.FindMatch(product.Description, List_WVA_Products.ListProducts, Convert.ToDouble(MinScoreAdjustSlider.Value));

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

        private void AddMenuItems()
        {
            try
            {
                // Reset the products ContextMenu
                STP_ContextMenu.Items.Clear();

                // Sets 'ClickedIndex' to the index of selected cell
                if (STP_DataGrid.Items.IndexOf(STP_DataGrid.CurrentItem) > -1)
                {
                    ClickedIndex = STP_DataGrid.Items.IndexOf(STP_DataGrid.CurrentItem);
                }

                foreach (MatchProduct match in ListMatchedProducts[ClickedIndex])
                {
                    MenuItem menuItem = new MenuItem() { Header = match.Name };
                    menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                    STP_ContextMenu.Items.Add(menuItem);
                }
            }
            catch (Exception x)
            {

            }
        }

        //private void EditColumn(int column, int row, string cellText)
        //{
        //    switch (column)
        //    {
        //        case 0:
        //            ShipToPatientViewModel.ListPrescriptions[row].Patient = cellText;
        //            break;
        //        case 1:
        //            ShipToPatientViewModel.ListPrescriptions[row].Eye = cellText;
        //            break;
        //        //
        //        // Skip image row
        //        //
        //        case 3:
        //            ShipToPatientViewModel.ListPrescriptions[row].Product = cellText;
        //            break;
        //        case 4:
        //            ShipToPatientViewModel.ListPrescriptions[row].Quantity = cellText;
        //            break;
        //        case 5:
        //            ShipToPatientViewModel.ListPrescriptions[row].BaseCurve = cellText;
        //            break;
        //        case 6:
        //            ShipToPatientViewModel.ListPrescriptions[row].Diameter = cellText;
        //            break;
        //        case 7:
        //            ShipToPatientViewModel.ListPrescriptions[row].Sphere = cellText;
        //            break;
        //        case 8:
        //            ShipToPatientViewModel.ListPrescriptions[row].Cylinder = cellText;
        //            break;
        //        case 9:
        //            ShipToPatientViewModel.ListPrescriptions[row].Axis = cellText;
        //            break;
        //        case 10:
        //            ShipToPatientViewModel.ListPrescriptions[row].Add = cellText;
        //            break;
        //        case 11:
        //            ShipToPatientViewModel.ListPrescriptions[row].Color = cellText;
        //            break;
        //        default:
        //            break;
        //    }
        //}

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
      
        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedIndex = 0;
                MenuItem menuItem = sender as MenuItem;
                string product = menuItem.Header.ToString();

                // Get the selected menu item
                for (int i = 0; i < STP_ContextMenu.Items.Count; i++)
                {
                    MenuItem item = (MenuItem)STP_ContextMenu.Items[i];

                    if (item.Header.ToString() == product)
                    {
                        selectedIndex = i;
                        break;
                    }
                }

                // Get selected prescription object in data grid
                IList rows = STP_DataGrid.SelectedItems;
                Prescription prescription = (Prescription)rows[0];

                // Get selected row and column
                int row = STP_DataGrid.Items.IndexOf(STP_DataGrid.CurrentItem);
                int column = STP_DataGrid.CurrentColumn.DisplayIndex;

                // Only want to change 'Products' column 
                if (product != "No Matches Found" && column == 3)
                {
                    STP_DataGrid.GetCell(row, column).Content = product;
                    ShipToPatientViewModel.ListPrescriptions[row].Product = product;

                    if (ListMatchedProducts[row][selectedIndex].MatchScore > 92)
                    {
                        ShipToPatientViewModel.ListPrescriptions[row].ProductImagePath = @"C:\Users\evan\Desktop\Images\GreenBubble.png";
                    }
                    else if (ListMatchedProducts[row][selectedIndex].MatchScore > 75)
                    {
                        ShipToPatientViewModel.ListPrescriptions[row].ProductImagePath = @"C:\Users\evan\Desktop\Images\YellowBubble.png";
                    }
                    else
                    {
                        ShipToPatientViewModel.ListPrescriptions[row].ProductImagePath = @"C:\Users\evan\Desktop\Images\RedBubble.jpg";
                    }

                    STP_DataGrid.Items.Refresh();
                }
                else
                {
                    ShipToPatientViewModel.ListPrescriptions[row].ProductImagePath = @"C:\Users\evan\Desktop\Images\RedBubble.jpg";
                    STP_DataGrid.Items.Refresh();
                }
            }
            catch (Exception x)
            {

            }
        }
                
        private void STP_DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    //int column = STP_DataGrid.CurrentColumn.DisplayIndex;
                    int row = e.Row.GetIndex();
                    //var typedText = e.EditingElement as TextBox;
                    //string cellText = typedText.Text.Trim();

                    ShipToOfficeViewModel.ListPrescriptions[row] = (Prescription)STP_DataGrid.Items[row];
                   
                    //EditColumn(column, row, cellText);
                    SetContextMenuItems();
                    AddMenuItems();
                }
            }
            catch{}
        }

        private void STP_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetContextMenuItems();
            AddMenuItems();
        }

        private void MinScoreAdjustSlider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (STP_ContextMenu.Items.Count > 0)
            {
                MatchPercentLabel.Content = $"Match Percent: {Convert.ToInt16(MinScoreAdjustSlider.Value)}%";
                SetContextMenuItems();
                AddMenuItems();
            }
        }

        private void VerifyOrderButton_Click(object sender, RoutedEventArgs e)
        {
            List<ValidationDetail> listValidations = new List<ValidationDetail>();

            IList rows = STP_DataGrid.Items;

            for (int i = 0; i < STP_DataGrid.Items.Count; i++)
            {
                Prescription prescription = (Prescription)rows[i];

                listValidations.Add(new ValidationDetail()
                {
                    _PatientName = $"{prescription.Patient}",
                    _Eye = prescription.Eye,
                    _Quantity = prescription.Quantity.ToString(),
                    _Description = prescription.Product,
                    _Vendor = "",
                    _Price = "",
                    _ID = new ID() { Value = "1" },
                    _CustomerID = prescription._CustomerID,
                    _SKU = new SKU() { Value = "" },
                    _ProductKey = new ProductKey() { Value = prescription.ProductCode },
                    _UPC = new UPC() { Value = "" },
                    _BaseCurve = new BaseCurve() { Value = prescription.BaseCurve },
                    _Diameter = new Diameter() { Value = prescription.Diameter },
                    _Sphere = new Sphere() { Value = prescription.Sphere },
                    _Cylinder = new Cylinder() { Value = prescription.Cylinder },
                    _Axis = new Axis() { Value = prescription.Axis },
                    _Add = new Add() { Value = prescription.Add },
                    _Color = new Models.ProductParameters.Color() { Value = prescription.Color },
                    _Multifocal = new Multifocal() { Value = prescription.Multifocal }
                });
            }

            ValidationWrapper validationWrapper = new ValidationWrapper()
            {
                Request = new ProductValidation()
                {
                    // Assign user's key here 
                    Key = "426761f0-3e9d-4dfd-bdbf-0f35a232c285",
                    ProductsToValidate = new List<ItemDetail>()
                }
            };

            foreach (ValidationDetail validationDetail in listValidations)
            {
                validationWrapper.Request.ProductsToValidate.Add(new ValidationDetail(validationDetail));
            }

            string endpoint = "https://orders-qa.wisvis.com/validations";
            string strValidatedProducts = API.Post(endpoint, validationWrapper, out string httpStatus);
            var validatedProducts = JsonConvert.DeserializeObject<ValidationResponse>(strValidatedProducts);
            var prods = validatedProducts.Data.Products;

            for (int i = 0; i < prods.Count(); i++)
            {
                // if product is null or invalid, keep old value, else change it to the new value from prods
                Prescription prescription = new Prescription()
                {
                    // These properties don't need to be validated
                    ProductImagePath = ShipToPatientViewModel.ListPrescriptions[i].ProductImagePath,
                    IsChecked = ShipToPatientViewModel.ListPrescriptions[i].IsChecked,
                    FirstName = ShipToPatientViewModel.ListPrescriptions[i].FirstName,
                    LastName = ShipToPatientViewModel.ListPrescriptions[i].LastName,
                    Patient = ShipToPatientViewModel.ListPrescriptions[i].Patient,
                    Eye = ShipToPatientViewModel.ListPrescriptions[i].Eye,
                    Quantity = ShipToPatientViewModel.ListPrescriptions[i].Quantity,
                    Date = ShipToPatientViewModel.ListPrescriptions[i].Date,
                    _CustomerID = ShipToPatientViewModel.ListPrescriptions[i]._CustomerID,

                    // If prods[i].Property.Value == null change field to old value, else change to new value
                    Product = prods[i]._Description ?? ShipToPatientViewModel.ListPrescriptions[i].Product,
                    ProductCode = Validator.CheckIfValid(prods[i]._ProductKey) ? prods[i]._ProductKey?.Value : ShipToOfficeViewModel.ListPrescriptions[i].ProductCode,

                    // NOTE: to help explain ternary statements below for cell color
                    // IF property IS null or IS blank THEN cell = White 
                    // IF property isValid THEN cell = Green
                    // IF property NOT isValid THEN cell = Red
                    BaseCurve = Validator.CheckIfValid(prods[i]._BaseCurve) ? prods[i]._BaseCurve.Value : ShipToPatientViewModel.ListPrescriptions[i].BaseCurve,
                    BaseCurveCellColor = (prods[i]._BaseCurve?.Value == null || prods[i]._BaseCurve?.Value?.Trim() == "") && prods[i]._BaseCurve?.ErrorMessage == null ? "White" : prods[i]._BaseCurve.IsValid ? "Green" : "Red",

                    Diameter = Validator.CheckIfValid(prods[i]._Diameter) ? prods[i]._Diameter.Value : ShipToPatientViewModel.ListPrescriptions[i].Diameter,
                    DiameterCellColor = (prods[i]._Diameter?.Value == null || prods[i]._Diameter?.Value?.Trim() == "") && prods[i]._Diameter?.ErrorMessage == null ? "White" : prods[i]._Diameter.IsValid ? "Green" : "Red",

                    Sphere = Validator.CheckIfValid(prods[i]._Sphere) ? prods[i]._Sphere.Value : ShipToPatientViewModel.ListPrescriptions[i].Sphere,
                    SphereCellColor = (prods[i]._Sphere?.Value == null || prods[i]._Sphere?.Value?.Trim() == "") && prods[i]._Sphere?.ErrorMessage == null ? "White" : prods[i]._Sphere.IsValid ? "Green" : "Red",

                    Cylinder = Validator.CheckIfValid(prods[i]._Cylinder) ? prods[i]._Cylinder.Value : ShipToPatientViewModel.ListPrescriptions[i].Cylinder,
                    CylinderCellColor = (prods[i]._Cylinder?.Value == null || prods[i]._Cylinder?.Value?.Trim() == "") && prods[i]._Cylinder?.ErrorMessage == null ? "White" : prods[i]._Cylinder.IsValid ? "Green" : "Red",

                    Axis = Validator.CheckIfValid(prods[i]._Axis) ? prods[i]._Axis.Value : ShipToPatientViewModel.ListPrescriptions[i].Axis,
                    AxisCellColor = (prods[i]._Axis?.Value == null || prods[i]._Axis?.Value?.Trim() == "") && prods[i]._Axis?.ErrorMessage == null ? "White" : prods[i]._Axis.IsValid ? "Green" : "Red",

                    Add = Validator.CheckIfValid(prods[i]._Add) ? prods[i]._Add.Value : ShipToPatientViewModel.ListPrescriptions[i].Add,
                    AddCellColor = (prods[i]._Add?.Value == null || prods[i]._Add?.Value?.Trim() == "") && prods[i]._Add?.ErrorMessage == null ? "White" : prods[i]._Add.IsValid ? "Green" : "Red",

                    Color = Validator.CheckIfValid(prods[i]._Color) ? prods[i]._Color.Value : ShipToPatientViewModel.ListPrescriptions[i].Color,
                    ColorCellColor = (prods[i]._Color?.Value == null || prods[i]._Color?.Value?.Trim() == "") && prods[i]._Color?.ErrorMessage == null ? "White" : prods[i]._Color.IsValid ? "Green" : "Red",

                    Multifocal = Validator.CheckIfValid(prods[i]._Multifocal) ? prods[i]._Multifocal.Value : ShipToPatientViewModel.ListPrescriptions[i].Multifocal,
                    MultifocalCellColor = (prods[i]._Multifocal?.Value == null || prods[i]._Multifocal?.Value?.Trim() == "") && prods[i]._Multifocal?.ErrorMessage == null ? "White" : prods[i]._Multifocal.IsValid ? "Green" : "Red",
                };

                ShipToPatientViewModel.ListPrescriptions[i] = prescription;
            }

            STP_DataGrid.Items.Refresh();
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

        private void SubmitOrderButton_Click(object sender, RoutedEventArgs e)
        {
            List<Item> listItems = new List<Item>();
            IList rows = STP_DataGrid.Items;

            for (int i = 0; i < STP_DataGrid.Items.Count; i++)
            {
                Prescription prescription = (Prescription)rows[i];
                listItems.Add(new Item()
                {
                    ID = prescription.ID,
                    FirstName = prescription.FirstName,
                    LastName = prescription.LastName,
                    PatientID = prescription._CustomerID?.Value,
                    Eye = prescription.Eye,
                    Quantity = prescription.Quantity,
                    ItemRetailPrice = prescription.Price,
                    OrderDetail = new OrderDetail()
                    {
                        _SKU = new SKU() { Value = prescription.SKU },
                        _ProductKey = new ProductKey() { Value = prescription.ProductCode },
                        _UPC = new UPC() { Value = prescription.UPC },
                        _BaseCurve = new BaseCurve() { Value = prescription.BaseCurve },
                        _Diameter = new Diameter() { Value = prescription.Diameter },
                        _Sphere = new Sphere() { Value = prescription.Sphere },
                        _Cylinder = new Cylinder() { Value = prescription.Cylinder },
                        _Axis = new Axis() { Value = prescription.Axis },
                        _Add = new Add() { Value = prescription.Add },
                        _Color = new Models.ProductParameters.Color() { Value = prescription.Color },
                        _Multifocal = new Multifocal() { Value = prescription.Multifocal },
                    }
                });
            }

            OutOrderWrapper outOrderWrapper = new OutOrderWrapper()
            {
                OutOrder = new OutOrder()
                {
                    ApiKey = "426761f0-3e9d-4dfd-bdbf-0f35a232c285",
                    PatientOrder = new Order()
                    {
                        CustomerID = "",
                        ID = "",
                        DoB = "",
                        Name_1 = "",
                        Name_2 = "",
                        StreetAddr_1 = "",
                        StreetAddr_2 = "",
                        City = "",
                        Zip = "",
                        ShipToAccount = "",
                        OfficeName = "",
                        OrderedBy = "",
                        PoNumber = "",
                        ShippingMethod = ShippingTypeComboBox.Text,
                        ShipToPatient = "Y",
                        Freight = "",
                        Tax = "",
                        Discount = "",
                        InvoiceTotal = "",
                        Email = "",
                        Items = listItems
                    }
                }
            };
        }
    }
}
