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
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Cryptography;
using WVA_Compulink_Integration.MatchFinder;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Models.Order;
using WVA_Compulink_Integration.Models.Order.Out;
using WVA_Compulink_Integration.Models.Patient;
using WVA_Compulink_Integration.Models.Prescription;
using WVA_Compulink_Integration.Models.Product;
using WVA_Compulink_Integration.Models.Product.ProductIn;
using WVA_Compulink_Integration.Models.ProductParameters;
using WVA_Compulink_Integration.Models.Validations;
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
        public int SelectedRow { get; set; }
        public int SelectedColumn { get; set; }
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
            FindProductMatches();
            SetMenuItems();
            MatchPercentLabel.Content = $"Match Percent: {MinScoreAdjustSlider.Value}%";
        }

        private void FindProductMatches()
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
        
        private void SetMenuItems()
        {
            try
            {
                // Reset the products ContextMenu
                STO_ProductContextMenu.Items.Clear();

                // Sets 'ClickedIndex' to the index of selected cell
                if (STO_DataGrid.Items.IndexOf(STO_DataGrid.CurrentItem) > -1)
                {
                    SelectedRow = STO_DataGrid.Items.IndexOf(STO_DataGrid.CurrentItem);
                    SelectedColumn = STO_DataGrid.CurrentColumn.DisplayIndex;
                }

                // If column IS 'BaseCurve'
                if (SelectedColumn == 5)
                {
                    try
                    {
                        int ValidItemsCount = ShipToOfficeViewModel.ListPrescriptions[SelectedRow].BaseCurveValidItems.Count;

                        if (ValidItemsCount > 0)
                        {
                            for (int i = 0; i < ValidItemsCount; i++)
                            {
                                MenuItem menuItem = new MenuItem() { Header = ShipToOfficeViewModel.ListPrescriptions[SelectedRow].BaseCurveValidItems[i] };
                                menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                                STO_ProductContextMenu.Items.Add(menuItem);
                            }
                        }
                        else
                        {
                            throw new Exception("No Valid Items");
                        }
                    }
                    catch (Exception x)
                    {
                        MenuItem menuItem = new MenuItem() { Header = "Not Available" };
                        menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                        STO_ProductContextMenu.Items.Add(menuItem);
                    }                                              
                }
                // If column IS 'Diameter'
                else if (SelectedColumn == 6)
                {
                    try
                    {
                        int ValidItemsCount = ShipToOfficeViewModel.ListPrescriptions[SelectedRow].DiameterValidItems.Count;

                        if (ValidItemsCount > 0)
                        {
                            for (int i = 0; i < ValidItemsCount; i++)
                            {
                                MenuItem menuItem = new MenuItem() { Header = ShipToOfficeViewModel.ListPrescriptions[SelectedRow].DiameterValidItems[i] };
                                menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                                STO_ProductContextMenu.Items.Add(menuItem);
                            }
                        }
                        else
                        {
                            throw new Exception("No Valid Items");
                        }
                    }
                    catch (Exception x)
                    {
                        MenuItem menuItem = new MenuItem() { Header = "Not Available" };
                        menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                        STO_ProductContextMenu.Items.Add(menuItem);
                    }
                }
                // If column IS 'Sphere'
                else if (SelectedColumn == 7)
                {
                    try
                    {
                        int ValidItemsCount = ShipToOfficeViewModel.ListPrescriptions[SelectedRow].SphereValidItems.Count;

                        if (ValidItemsCount > 0)
                        {
                            for (int i = 0; i < ValidItemsCount; i++)
                            {
                                MenuItem menuItem = new MenuItem() { Header = ShipToOfficeViewModel.ListPrescriptions[SelectedRow].SphereValidItems[i] };
                                menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                                STO_ProductContextMenu.Items.Add(menuItem);
                            }
                        }
                        else
                        {
                            throw new Exception("No Valid Items");
                        }
                    }
                    catch (Exception x)
                    {
                        MenuItem menuItem = new MenuItem() { Header = "Not Available" };
                        menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                        STO_ProductContextMenu.Items.Add(menuItem);
                    }
                }
                // If column IS 'Cylinder'
                else if (SelectedColumn == 8)
                {
                    try
                    {
                        int ValidItemsCount = ShipToOfficeViewModel.ListPrescriptions[SelectedRow].CylinderValidItems.Count;

                        if (ValidItemsCount > 0)
                        {
                            for (int i = 0; i < ValidItemsCount; i++)
                            {
                                MenuItem menuItem = new MenuItem() { Header = ShipToOfficeViewModel.ListPrescriptions[SelectedRow].CylinderValidItems[i] };
                                menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                                STO_ProductContextMenu.Items.Add(menuItem);
                            }
                        }
                        else
                        {
                            throw new Exception("No Valid Items");
                        }
                    }
                    catch (Exception x)
                    {
                        MenuItem menuItem = new MenuItem() { Header = "Not Available" };
                        menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                        STO_ProductContextMenu.Items.Add(menuItem);
                    }
                }
                // If column IS 'Axis'
                else if (SelectedColumn == 9)
                {
                    try
                    {
                        int ValidItemsCount = ShipToOfficeViewModel.ListPrescriptions[SelectedRow].AxisValidItems.Count;

                        if (ValidItemsCount > 0)
                        {
                            for (int i = 0; i < ValidItemsCount; i++)
                            {
                                MenuItem menuItem = new MenuItem() { Header = ShipToOfficeViewModel.ListPrescriptions[SelectedRow].AxisValidItems[i] };
                                menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                                STO_ProductContextMenu.Items.Add(menuItem);
                            }
                        }
                        else
                        {
                            throw new Exception("No Valid Items");
                        }
                    }
                    catch (Exception x)
                    {
                        MenuItem menuItem = new MenuItem() { Header = "Not Available" };
                        menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                        STO_ProductContextMenu.Items.Add(menuItem);
                    }
                }
                // If column IS 'Add'
                else if (SelectedColumn == 10)
                {
                    try
                    {
                        int ValidItemsCount = ShipToOfficeViewModel.ListPrescriptions[SelectedRow].AddValidItems.Count;

                        if (ValidItemsCount > 0)
                        {
                            for (int i = 0; i < ValidItemsCount; i++)
                            {
                                MenuItem menuItem = new MenuItem() { Header = ShipToOfficeViewModel.ListPrescriptions[SelectedRow].AddValidItems[i] };
                                menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                                STO_ProductContextMenu.Items.Add(menuItem);
                            }
                        }
                        else
                        {
                            throw new Exception("No Valid Items");
                        }
                    }
                    catch (Exception x)
                    {
                        MenuItem menuItem = new MenuItem() { Header = "Not Available" };
                        menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                        STO_ProductContextMenu.Items.Add(menuItem);
                    }
                }
                // If column IS 'Color'
                else if (SelectedColumn == 11)
                {
                    try
                    {
                        int ValidItemsCount = ShipToOfficeViewModel.ListPrescriptions[SelectedRow].ColorValidItems.Count;

                        if (ValidItemsCount > 0)
                        {
                            for (int i = 0; i < ValidItemsCount; i++)
                            {
                                MenuItem menuItem = new MenuItem() { Header = ShipToOfficeViewModel.ListPrescriptions[SelectedRow].ColorValidItems[i] };
                                menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                                STO_ProductContextMenu.Items.Add(menuItem);
                            }
                        }
                        else
                        {
                            throw new Exception("No Valid Items");
                        }
                    }
                    catch (Exception x)
                    {
                        MenuItem menuItem = new MenuItem() { Header = "Not Available" };
                        menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                        STO_ProductContextMenu.Items.Add(menuItem);
                    }
                }
                // If column IS 'Multifocal'
                else if (SelectedColumn == 12)
                {
                    try
                    {
                        int ValidItemsCount = ShipToOfficeViewModel.ListPrescriptions[SelectedRow].MultifocalValidItems.Count;

                        if (ValidItemsCount > 0)
                        {
                            for (int i = 0; i < ValidItemsCount; i++)
                            {
                                MenuItem menuItem = new MenuItem() { Header = ShipToOfficeViewModel.ListPrescriptions[SelectedRow].MultifocalValidItems[i] };
                                menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                                STO_ProductContextMenu.Items.Add(menuItem);
                            }
                        }
                        else
                        {
                            throw new Exception("No Valid Items");
                        }
                    }
                    catch (Exception x)
                    {
                        MenuItem menuItem = new MenuItem() { Header = "Not Available" };
                        menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                        STO_ProductContextMenu.Items.Add(menuItem);
                    }
                }              
                // Normal match product list
                else
                {
                    foreach (MatchProduct match in ListMatchedProducts[SelectedRow])
                    {
                        MenuItem menuItem = new MenuItem() { Header = match.Name };
                        menuItem.Click += new RoutedEventHandler(ContextMenu_Click);
                        STO_ProductContextMenu.Items.Add(menuItem);
                    }
                }
            }
            catch (Exception x)
            {

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
        
        private string AssignCellColor(string prodValue, bool isValid, string errorMessage, bool canBeValidated)
        {      
            if (prodValue == null || prodValue == "" && errorMessage == null)
            {
                return "White";
            }
            else if (!canBeValidated)
            {
                return "Yellow";
            }
            else if (isValid)
            {
                return "Green";
            }
            else
            {
                return "Red";
            }
        }

        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedIndex = 0;
                MenuItem menuItem = sender as MenuItem;
                string selectedItem = menuItem.Header.ToString();

                // Get the selected menu item
                for (int i = 0; i < STO_ProductContextMenu.Items.Count; i++)
                {
                    MenuItem item = (MenuItem)STO_ProductContextMenu.Items[i];
                    
                    if (item.Header.ToString() == selectedItem)
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
                if (selectedItem != "No Matches Found" && selectedItem != "Not Available")
                {
                    STO_DataGrid.GetCell(row, column).Content = selectedItem;

                    if (column <= 4)
                    {
                        ShipToOfficeViewModel.ListPrescriptions[row].Product = selectedItem;

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
                    }

                    if (column == 5)
                    {
                        ShipToOfficeViewModel.ListPrescriptions[row].BaseCurve = selectedItem;
                    }
                    if (column == 6)
                    {
                        ShipToOfficeViewModel.ListPrescriptions[row].Diameter = selectedItem;
                    }
                    if (column == 7)
                    {
                        ShipToOfficeViewModel.ListPrescriptions[row].Sphere = selectedItem;
                    }
                    if (column == 8)
                    {
                        ShipToOfficeViewModel.ListPrescriptions[row].Cylinder = selectedItem;
                    }
                    if (column == 9)
                    {
                        ShipToOfficeViewModel.ListPrescriptions[row].Axis = selectedItem;
                    }
                    if (column == 10)
                    {
                        ShipToOfficeViewModel.ListPrescriptions[row].Add = selectedItem;
                    }
                    if (column == 11)
                    {
                        ShipToOfficeViewModel.ListPrescriptions[row].Color = selectedItem;
                    }
                    if (column == 12)
                    {
                        ShipToOfficeViewModel.ListPrescriptions[row].Multifocal = selectedItem;
                    }

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
                    int row = e.Row.GetIndex();
                 
                    FindProductMatches();
                    SetMenuItems();
                }
            }
            catch (Exception x)
            {

            }
        }

        private void STO_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {          
            FindProductMatches();
            SetMenuItems();
        }

        private void MinScoreAdjustSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (STO_ProductContextMenu.Items.Count > 0)
            {
                MatchPercentLabel.Content = $"Match Percent: {Convert.ToInt16(MinScoreAdjustSlider.Value)}%";
                FindProductMatches();
                SetMenuItems();
            }
        }

        private void VerifyOrderButton_Click(object sender, RoutedEventArgs e)
        {
            List<ValidationDetail> listValidations = new List<ValidationDetail>();

            IList rows = STO_DataGrid.Items;

            for (int i=0; i<STO_DataGrid.Items.Count; i++)
            {
                Prescription prescription = (Prescription)rows[i];

                listValidations.Add(new ValidationDetail()
                {
                    _PatientName = $"{prescription.Patient}" ,
                    _Eye = prescription.Eye,
                    _Quantity =  prescription.Quantity.ToString(),
                    _Description = prescription.Product,
                    _Vendor = "",
                    _Price = "",
                    _ID = new ID() { Value = "1"},
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
                    _Multifocal = new Multifocal() { Value = prescription.Multifocal },            
                });
            }

            ValidationWrapper validationWrapper = new ValidationWrapper()
            {
                Request = new ProductValidation()
                {
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
                    ProductImagePath    =   ShipToOfficeViewModel.ListPrescriptions[i].ProductImagePath,
                    IsChecked           =   ShipToOfficeViewModel.ListPrescriptions[i].IsChecked,
                    FirstName           =   ShipToOfficeViewModel.ListPrescriptions[i].FirstName,
                    LastName            =   ShipToOfficeViewModel.ListPrescriptions[i].LastName,
                    Patient             =   ShipToOfficeViewModel.ListPrescriptions[i].Patient,
                    Eye                 =   ShipToOfficeViewModel.ListPrescriptions[i].Eye,
                    Quantity            =   ShipToOfficeViewModel.ListPrescriptions[i].Quantity,
                    Date                =   ShipToOfficeViewModel.ListPrescriptions[i].Date,
                    _CustomerID         =   ShipToOfficeViewModel.ListPrescriptions[i]._CustomerID,

                    // If prods[i].Property.Value == null change field to old value, else change to new value
                    CanBeValidated      =   prods[i].CanBeValidated,
                    Product             =   prods[i]._Description ?? ShipToOfficeViewModel.ListPrescriptions[i].Product,
                    ProductCode         =   Validator.CheckIfValid(prods[i]._ProductKey) ? prods[i]._ProductKey?.Value : ShipToOfficeViewModel.ListPrescriptions[i].ProductCode,

                    // NOTE: to help explain ternary statements below for cell colors
                    // IF property IS null or IS blank AND errorMessage IS null THEN cell = White 
                    // IF property isValid THEN cell = Green
                    // IF property NOT isValid THEN cell = Red
                    BaseCurveValidItems = prods[i]._BaseCurve.ValidItems,
                    BaseCurve = Validator.CheckIfValid(prods[i]._BaseCurve)         ?       prods[i]._BaseCurve.Value : ShipToOfficeViewModel.ListPrescriptions[i].BaseCurve,                   
                    BaseCurveCellColor = AssignCellColor(prodValue: prods[i]._BaseCurve?.Value?.Trim(), isValid: prods[i]._BaseCurve.IsValid, errorMessage: prods[i]._BaseCurve.ErrorMessage, canBeValidated: prods[i].CanBeValidated), 

                    DiameterValidItems = prods[i]._Diameter.ValidItems,
                    Diameter = Validator.CheckIfValid(prods[i]._Diameter)           ?       prods[i]._Diameter.Value : ShipToOfficeViewModel.ListPrescriptions[i].Diameter,
                    DiameterCellColor = AssignCellColor(prodValue: prods[i]._Diameter?.Value?.Trim(), isValid: prods[i]._Diameter.IsValid, errorMessage: prods[i]._Diameter.ErrorMessage, canBeValidated: prods[i].CanBeValidated),

                    SphereValidItems = prods[i]._Sphere.ValidItems,
                    Sphere = Validator.CheckIfValid(prods[i]._Sphere)               ?       prods[i]._Sphere.Value : ShipToOfficeViewModel.ListPrescriptions[i].Sphere,
                    SphereCellColor = AssignCellColor(prodValue: prods[i]._Sphere?.Value?.Trim(), isValid: prods[i]._Sphere.IsValid, errorMessage: prods[i]._Sphere.ErrorMessage, canBeValidated: prods[i].CanBeValidated),

                    CylinderValidItems = prods[i]._Cylinder.ValidItems,
                    Cylinder = Validator.CheckIfValid(prods[i]._Cylinder)           ?       prods[i]._Cylinder.Value : ShipToOfficeViewModel.ListPrescriptions[i].Cylinder,
                    CylinderCellColor = AssignCellColor(prodValue: prods[i]._Cylinder?.Value?.Trim(), isValid: prods[i]._Cylinder.IsValid, errorMessage: prods[i]._Cylinder.ErrorMessage, canBeValidated: prods[i].CanBeValidated),

                    AxisValidItems = prods[i]._Axis.ValidItems,
                    Axis = Validator.CheckIfValid(prods[i]._Axis)                   ?       prods[i]._Axis.Value : ShipToOfficeViewModel.ListPrescriptions[i].Axis,
                    AxisCellColor = AssignCellColor(prodValue: prods[i]._Axis?.Value?.Trim(), isValid: prods[i]._Axis.IsValid, errorMessage: prods[i]._Axis.ErrorMessage, canBeValidated: prods[i].CanBeValidated),

                    AddValidItems = prods[i]._Add.ValidItems,
                    Add = Validator.CheckIfValid(prods[i]._Add)                     ?       prods[i]._Add.Value : ShipToOfficeViewModel.ListPrescriptions[i].Add,
                    AddCellColor = AssignCellColor(prodValue: prods[i]._Add?.Value?.Trim(), isValid: prods[i]._Add.IsValid, errorMessage: prods[i]._Add.ErrorMessage, canBeValidated: prods[i].CanBeValidated),

                    ColorValidItems = prods[i]._Color.ValidItems,
                    Color = Validator.CheckIfValid(prods[i]._Color)                 ?       prods[i]._Color.Value : ShipToOfficeViewModel.ListPrescriptions[i].Color,
                    ColorCellColor = AssignCellColor(prodValue: prods[i]._Color?.Value?.Trim(), isValid: prods[i]._Color.IsValid, errorMessage: prods[i]._Color.ErrorMessage, canBeValidated: prods[i].CanBeValidated),

                    MultifocalValidItems = prods[i]._Multifocal.ValidItems,
                    Multifocal = Validator.CheckIfValid(prods[i]._Multifocal)       ?       prods[i]._Multifocal.Value : ShipToOfficeViewModel.ListPrescriptions[i].Multifocal,
                    MultifocalCellColor = AssignCellColor(prodValue: prods[i]._Multifocal?.Value?.Trim(), isValid: prods[i]._Multifocal.IsValid, errorMessage: prods[i]._Multifocal.ErrorMessage, canBeValidated: prods[i].CanBeValidated),
                };

                ShipToOfficeViewModel.ListPrescriptions[i] = prescription;
            }

            STO_DataGrid.Items.Refresh();

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

        private void SubmitOrderButton_Click(object sender, RoutedEventArgs e)
        {
            List<Item> listItems = new List<Item>();
            IList rows = STO_DataGrid.Items;

            for (int i = 0; i < STO_DataGrid.Items.Count; i++)
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
