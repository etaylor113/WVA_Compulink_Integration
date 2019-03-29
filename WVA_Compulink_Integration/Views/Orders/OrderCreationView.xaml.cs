using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.MatchFinder;
using WVA_Compulink_Integration.Models.Order;
using WVA_Compulink_Integration.Models.Order.Out;
using WVA_Compulink_Integration.Models.Prescription;
using WVA_Compulink_Integration.Models.Product;
using WVA_Compulink_Integration.Models.ProductParameters;
using WVA_Compulink_Integration.Models.Validations;
using WVA_Compulink_Integration.Utility.UI_Tools;
using WVA_Compulink_Integration.ViewModels.Orders;
using WVA_Compulink_Integration.Error;
using System.Windows.Input;
using WVA_Compulink_Integration.Views.Search;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Models;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;
using WVA_Compulink_Integration.Utility.File;
using WVA_Compulink_Integration.Models.Patient;
using System.Diagnostics;
using WVA_Compulink_Integration.Models.Response;
using WVA_Compulink_Integration.MatchFinder.ProductPredictions;
using System.Threading;
using System.ComponentModel;

namespace WVA_Compulink_Integration.Views.Orders
{
    /// <summary>
    /// Interaction logic for WVA_OrderView.xaml
    /// </summary>
    public partial class OrderCreationView : UserControl
    {
        // Keeps track of what product in DataGrid user has clicked on
        public int SelectedRow { get; set; }
        public int SelectedColumn { get; set; }
        public static string ViewMode { get; set; }
        public List<List<MatchProduct>> ListMatchedProducts = new List<List<MatchProduct>>();
        public List<string> ListWVA_Products = new List<string>();


        public OrderCreationView()
        {
            InitializeComponent();
            SetUpUI();
        }

        private void SetUpUI()
        {
            try
            {
                MatchPercentLabel.Content = $"Match Percent: {Convert.ToInt16(MinScoreAdjustSlider.Value)}%";
                SetUpOrdersDataGrid();
                CheckViewMode();
                FindProductMatches();
                Verify();

                throw new Exception("TEST Exception");
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
            }
        }

        private void AutosaveOrder()
        {
            new Thread(SaveOrderAsync).Start();
        }

        private void SaveOrderAsync()
        {
            int numEr = 0;
            while (true)
            {
                try
                {
                    Dispatcher.Invoke(() =>
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        OutOrderWrapper order = GetCompleteOrder();
                        OrderCreationViewModel.SaveOrder(order);
                        Mouse.OverrideCursor = Cursors.Arrow;
                    });
                }
                catch (Exception ex)
                {
                    numEr++;

                    if (numEr >= 5)
                        AppError.ReportOrWrite(ex);
                }
                finally
                {
                    // Wait 2 minutes before auto-saving again
                    Thread.Sleep(120000);
                }
            }
        }

        private void CheckViewMode()
        {         
            if (OrderCreationViewModel.Order != null)
            {
                ViewMode = "edit";               
                SetUpEditOrder();
            }                       
            else if (OrderCreationViewModel.Prescriptions != null)
            {
                ViewMode = "new";
                SetUpNewOrder();
            }           
        }

        // =======================================================================================================================
        // ================================== Match Algorithm ====================================================================
        // =======================================================================================================================

        private void FindProductMatches(double matchScore = 50, bool overrideNumPicks = false)
        {
            double MatchScore;
            if (overrideNumPicks)
                MatchScore = matchScore;
            else
                MatchScore = Convert.ToDouble(MinScoreAdjustSlider.Value);

            // Reset list of matched products 
            ListMatchedProducts.Clear();

            // Get product names in DataGrid
            List<Product> compulinkProducts = new List<Product>();
            for (int i = 0; i < OrdersDataGrid.Items.Count; i++)
            {
                Prescription prescription = (Prescription)OrdersDataGrid.Items[i];
                compulinkProducts.Add(new Product() { Description = prescription.Product });
            }

            // Loop through product names list and pass each one into matcher algorithm 
            int index = 0;
            foreach (Product product in compulinkProducts)
            {
                List<Product> wvaProducts = WvaProducts.ListProducts;

                if (wvaProducts == null || wvaProducts.Count == 0)
                    throw new Exception("List<WVA_Products> is null or empty!");
             
                // Run match finder for product and return results based on numPicks (number of times same product has been chosen)
                List<MatchProduct> matchProducts = ProductPrediction.GetPredictionMatches(product.Description + product.Vendor, MatchScore, wvaProducts, overrideNumPicks);

                if (matchProducts?.Count > 0)
                {
                    ListMatchedProducts.Add(matchProducts);
                    OrderCreationViewModel.Prescriptions[index].ProductCode = matchProducts[0].ProductKey;
                }
                else
                    ListMatchedProducts.Add(new List<MatchProduct> { new MatchProduct("No Matches Found", 0) });

                index++;
            }
        }

        // =======================================================================================================================
        // ================================== UI Related Mehtods =================================================================
        // =======================================================================================================================

        private void SetUpShippingComboBox()
        {
            foreach (string shipType in ShippingTypes.ListShippingTypes)
            {
                ShippingTypeComboBox.Items.Add(shipType);
            }
        }

        private string GetShippingTypeID(string shipType)
        {
            switch (shipType)
            {
                case "Standard":
                    return "1";
                case "UPS Ground":
                    return "D";
                case "UPS 2nd Day Air":
                    return "J";
                case "UPS Next Day Air":
                    return "P";
                default:
                    return shipType;                  
            }
        }
        
        private void SetUpOrdersDataGrid()
        {
            OrdersDataGrid.ItemsSource = OrderCreationViewModel.Prescriptions;        
        }

        private string AssignCellColor(string prodValue, bool isValid, string errorMessage, bool canBeValidated)
        {
            if (prodValue == null && errorMessage == null || prodValue == "" && errorMessage == null)
                return "White";
            else if (!canBeValidated)
                return "Yellow";
            else if (isValid)
                return "Green";
            else
                return "Red";
        }

        private void AutoFillStpItems()
        {
            AddresseeTextBox.Text = OrderCreationViewModel.Order.Name_1;
            AddressTextBox.Text = OrderCreationViewModel.Order.StreetAddr_1;
            Suite_AptTextBox.Text = OrderCreationViewModel.Order.StreetAddr_2;
            CityTextBox.Text = OrderCreationViewModel.Order.City;
            StateComboBox.Text = OrderCreationViewModel.Order.State;
            ZipTextBox.Text = OrderCreationViewModel.Order.Zip;
            PhoneTextBox.Text = OrderCreationViewModel.Order.Phone;
            DoBTextBox.Text = OrderCreationViewModel.Order.DoB;
        }

        private void HideStpItems()
        {
            AddresseeLabel.Visibility = Visibility.Hidden;
            AddresseeTextBox.Visibility = Visibility.Hidden;
            AddressLabel.Visibility = Visibility.Hidden;
            AddressTextBox.Visibility = Visibility.Hidden;
            Suite_AptLabel.Visibility = Visibility.Hidden;
            Suite_AptTextBox.Visibility = Visibility.Hidden;
            CityLabel.Visibility = Visibility.Hidden;
            CityTextBox.Visibility = Visibility.Hidden;
            StateLabel.Visibility = Visibility.Hidden;
            StateComboBox.Visibility = Visibility.Hidden;
            ZipLabel.Visibility = Visibility.Hidden;
            ZipTextBox.Visibility = Visibility.Hidden;
            PhoneLabel.Visibility = Visibility.Hidden;
            PhoneTextBox.Visibility = Visibility.Hidden;
            DoBLabel.Visibility = Visibility.Hidden;
            DoBTextBox.Visibility = Visibility.Hidden;
        }

        private void ShowStpItems()
        {
            AddresseeLabel.Visibility = Visibility.Visible;
            AddresseeTextBox.Visibility = Visibility.Visible;
            AddressLabel.Visibility = Visibility.Visible;
            AddressTextBox.Visibility = Visibility.Visible;
            Suite_AptLabel.Visibility = Visibility.Visible;
            Suite_AptTextBox.Visibility = Visibility.Visible;
            CityLabel.Visibility = Visibility.Visible;
            CityTextBox.Visibility = Visibility.Visible;
            StateLabel.Visibility = Visibility.Visible;
            StateComboBox.Visibility = Visibility.Visible;
            ZipLabel.Visibility = Visibility.Visible;
            ZipTextBox.Visibility = Visibility.Visible;
            PhoneLabel.Visibility = Visibility.Visible;
            PhoneTextBox.Visibility = Visibility.Visible;
            //DoBLabel.Visibility = Visibility.Visible;
            //DoBTextBox.Visibility = Visibility.Visible;
        }

        private void ClearView()
        {
            // Empty datagrid
            OrderCreationViewModel.Prescriptions.Clear();
            OrdersDataGrid.Items.Refresh();

            // Clear right column
            OrderNameTextBox.Text = "";
            AccountIDTextBox.Text = "";
            OrderedByTextBox.Text = "";
            //OfficeNameTextBox.Text = "";
            PoNumberTextBox.Text = "";

            // Clear left column
            AddresseeTextBox.Text = "";
            AddressTextBox.Text = "";
            Suite_AptTextBox.Text = "";
            CityTextBox.Text = "";
            StateComboBox.Text = "";
            ZipTextBox.Text = "";
            PhoneTextBox.Text = "";
            DoBTextBox.Text = "";
        }

        private void SetUpStpFields()
        {
            try
            {
                string dsn = UserData.Data?.DSN;
                string id = OrderCreationViewModel.Prescriptions[0]._CustomerID?.Value;

                if (id == null)
                    return;

                string endpoint = $"http://{dsn}/api/patient/" + $"{id}";
                string strResponse = API.Get(endpoint, out string httpStatus);
                var patient = JsonConvert.DeserializeObject<Patient>(strResponse);

                if (patient != null)
                {
                    CityTextBox.Text = patient.City;
                    StateComboBox.Text = patient.State;
                    AddressTextBox.Text = patient.Street;
                    AddresseeTextBox.Text = patient.FullName;
                    ZipTextBox.Text = patient.Zip.Replace("-", "");
                    DoBTextBox.Text = patient.DoB;
                    PhoneTextBox.Text = patient.Phone;
                }
                else
                {
                    throw new NullReferenceException();
                }
              
            }
            catch (Exception x)
            {
                AppError.ReportOrWrite(x);
            }           
        }

        private void SetUpNewOrder()
        {
            // Autofill some user information      
            OrderNameTextBox.Text = OrderCreationViewModel.OrderName ?? "";
            AccountIDTextBox.Text = UserData.Data?.Account ?? "";
            OrderedByTextBox.Text = UserData.Data?.UserName ?? "";

            // Hide STP fields if order not STP
            if (!OrderCreationViewModel.Prescriptions[0].IsShipToPatient)
                HideStpItems();
            else
                SetUpStpFields();
        }

        private void SetUpEditOrder()
        {
            if (OrderCreationViewModel.Order.ShipToPatient == "Y")
            {
                ShowStpItems();
                AutoFillStpItems();
            }
            else
            {
                HideStpItems();
            }

            // Left column
            AddresseeTextBox.Text = OrderCreationViewModel.Order.Name_1 ?? "";
            AddressTextBox.Text = OrderCreationViewModel.Order.StreetAddr_1 ?? "";
            Suite_AptTextBox.Text = OrderCreationViewModel.Order.StreetAddr_2 ?? "";
            CityTextBox.Text = OrderCreationViewModel.Order.City ?? "";
            StateComboBox.Text = OrderCreationViewModel.Order.State ?? "";
            ZipTextBox.Text = OrderCreationViewModel.Order.Zip ?? "";
            PhoneTextBox.Text = OrderCreationViewModel.Order.Phone ?? "";
            DoBTextBox.Text = OrderCreationViewModel.Order.DoB ?? "";
            
            // Right column
            OrderNameTextBox.Text = OrderCreationViewModel.Order.OrderName ?? "";
            AccountIDTextBox.Text = UserData.Data?.Account;
            OrderedByTextBox.Text = OrderCreationViewModel.Order.OrderedBy ?? "";
            //OfficeNameTextBox.Text = OrderCreationViewModel.Order.OfficeName ?? "";
            PoNumberTextBox.Text = OrderCreationViewModel.Order.PoNumber ?? "";
            ShippingTypeComboBox.Text = OrderCreationViewModel.Order.ShippingMethod ?? "";

            // If there are no items then exit
            if (OrderCreationViewModel.Order.Items == null || OrderCreationViewModel.Order.Items?.Count == 0)
                return;

            // Datagrid rows
            for (int i = 0; i < OrderCreationViewModel.Order.Items.Count; i++)
            {
                Prescription prescription = new Prescription()
                {
                    // If product has been reviewed, show 'checked' image next to product name
                    ProductImagePath = OrderCreationViewModel.Order.Items[i].ProductDetail.ProductReviewed ? @"/Resources/CheckMarkCircle.png" : null,
                    FirstName = OrderCreationViewModel.Order.Items[i].FirstName,
                    _CustomerID = new CustomerID() { Value = OrderCreationViewModel.Order.Items[i].PatientID }, 
                    LastName = OrderCreationViewModel.Order.Items[i].LastName,
                    Eye = OrderCreationViewModel.Order.Items[i].Eye,
                    Quantity = OrderCreationViewModel.Order.Items[i].Quantity,
                    Product = OrderCreationViewModel.Order.Items[i].ProductDetail.Name,
                    ProductCode = OrderCreationViewModel.Order.Items[i].ProductDetail.ProductKey,
                    BaseCurve = OrderCreationViewModel.Order.Items[i].ProductDetail.BaseCurve,
                    Diameter = OrderCreationViewModel.Order.Items[i].ProductDetail.Diameter,
                    Sphere = OrderCreationViewModel.Order.Items[i].ProductDetail.Sphere,
                    Cylinder = OrderCreationViewModel.Order.Items[i].ProductDetail.Cylinder,
                    Axis = OrderCreationViewModel.Order.Items[i].ProductDetail.Axis,
                    Add = OrderCreationViewModel.Order.Items[i].ProductDetail.Add,
                    Color = OrderCreationViewModel.Order.Items[i].ProductDetail.Color,
                    Multifocal = OrderCreationViewModel.Order.Items[i].ProductDetail.Multifocal
                };
                OrderCreationViewModel.Prescriptions.Add(prescription);
            }
        }

        private void SetMenuItems()
        {
            try
            {                
                // If products list isn't loaded or an error occurs in DescriptionMatcher and 
                // 'ListMatchedProducts' can't be loaded then leave so no errors will occur
                if (ListMatchedProducts == null || ListMatchedProducts.Count == 0)
                    return;

                // Reset the products ContextMenu
                WVA_OrdersContextMenu.Items.Clear();
               
                // Sets 'ClickedIndex' to the index of selected cell
                int index = OrdersDataGrid.Items.IndexOf(OrdersDataGrid.CurrentItem);
                int idx = OrdersDataGrid.Items.IndexOf(OrdersDataGrid.SelectedItem);
                if (index > -1)
                {
                    SelectedRow = OrdersDataGrid.Items.IndexOf(OrdersDataGrid.CurrentItem);                
                    SelectedColumn = OrdersDataGrid.CurrentColumn.DisplayIndex;
                }
                else
                {
                    Trace.WriteLine("Couldn't get index in SetMenuItems()!");
                    return;
                }

                int ValidItemsCount;

                try
                {
                    // Load the context menu with relevant items at a specific column
                    // 0-2 are skipped because they are not editable parameters
                    switch (SelectedColumn)
                    {
                        case 3: // If column is a 'Product'
                            foreach (MatchProduct match in ListMatchedProducts[SelectedRow])
                            {
                                MenuItem menuItem = new MenuItem() { Header = match.Name };
                                menuItem.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                                WVA_OrdersContextMenu.Items.Add(menuItem);                               
                            }

                            // Add 'More Matches' item to the end of the list 
                            MenuItem moreMatchesMenuItem = new MenuItem() { Header = "-- More Matches --" };
                            moreMatchesMenuItem.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                            WVA_OrdersContextMenu.Items.Add(moreMatchesMenuItem);
                            break;
                        case 5: // If column is a 'BaseCurve'
                            if (OrderCreationViewModel.Prescriptions[SelectedRow].BaseCurveValidItems != null)
                                ValidItemsCount = OrderCreationViewModel.Prescriptions[SelectedRow].BaseCurveValidItems.Count;
                            else
                            {
                                SetNotAvailableMenuItem();
                                return;
                            }

                            if (ValidItemsCount > 0)
                            {
                                for (int i = 0; i < ValidItemsCount; i++)
                                {
                                    MenuItem menuItem = new MenuItem() { Header = OrderCreationViewModel.Prescriptions[SelectedRow].BaseCurveValidItems[i] };
                                    menuItem.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                                    WVA_OrdersContextMenu.Items.Add(menuItem);
                                }
                            }
                            else
                                throw new Exception("No Valid Items");
                            break;
                        case 6: // If column is a 'Diameter'
                            if (OrderCreationViewModel.Prescriptions[SelectedRow].DiameterValidItems != null)
                                ValidItemsCount = OrderCreationViewModel.Prescriptions[SelectedRow].DiameterValidItems.Count;
                            else
                            {
                                SetNotAvailableMenuItem();
                                return;
                            }

                            if (ValidItemsCount > 0)
                            {
                                for (int i = 0; i < ValidItemsCount; i++)
                                {
                                    MenuItem menuItem = new MenuItem() { Header = OrderCreationViewModel.Prescriptions[SelectedRow].DiameterValidItems[i] };
                                    menuItem.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                                    WVA_OrdersContextMenu.Items.Add(menuItem);
                                }
                            }
                            else
                                throw new Exception("No Valid Items");
                            break;
                        case 7: // If column is a 'Sphere'
                            if (OrderCreationViewModel.Prescriptions[SelectedRow].SphereValidItems != null)
                                ValidItemsCount = OrderCreationViewModel.Prescriptions[SelectedRow].SphereValidItems.Count;
                            else
                            {
                                SetNotAvailableMenuItem();
                                return;
                            }

                            if (ValidItemsCount > 0)
                            {
                                for (int i = 0; i < ValidItemsCount; i++)
                                {
                                    MenuItem menuItem = new MenuItem() { Header = OrderCreationViewModel.Prescriptions[SelectedRow].SphereValidItems[i] };
                                    menuItem.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                                    WVA_OrdersContextMenu.Items.Add(menuItem);
                                }
                            }
                            else
                                throw new Exception("No Valid Items");
                            break;
                        case 8: // If column is a 'Cylinder'
                            if (OrderCreationViewModel.Prescriptions[SelectedRow].CylinderValidItems != null)
                                ValidItemsCount = OrderCreationViewModel.Prescriptions[SelectedRow].CylinderValidItems.Count;
                            else
                            {
                                SetNotAvailableMenuItem();
                                return;
                            }

                            if (ValidItemsCount > 0)
                            {
                                for (int i = 0; i < ValidItemsCount; i++)
                                {
                                    MenuItem menuItem = new MenuItem() { Header = OrderCreationViewModel.Prescriptions[SelectedRow].CylinderValidItems[i] };
                                    menuItem.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                                    WVA_OrdersContextMenu.Items.Add(menuItem);
                                }
                            }
                            else
                                throw new Exception("No Valid Items");
                            break;
                        case 9: // If column is a 'Axis'
                            if (OrderCreationViewModel.Prescriptions[SelectedRow].AxisValidItems != null)
                                ValidItemsCount = OrderCreationViewModel.Prescriptions[SelectedRow].AxisValidItems.Count;
                            else
                            {
                                SetNotAvailableMenuItem();
                                return;
                            }

                            if (ValidItemsCount > 0)
                            {
                                for (int i = 0; i < ValidItemsCount; i++)
                                {
                                    MenuItem menuItem = new MenuItem() { Header = OrderCreationViewModel.Prescriptions[SelectedRow].AxisValidItems[i] };
                                    menuItem.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                                    WVA_OrdersContextMenu.Items.Add(menuItem);
                                }
                            }
                            else
                                throw new Exception("No Valid Items");
                            break;
                        case 10: // If column is a 'Add'
                            if (OrderCreationViewModel.Prescriptions[SelectedRow].AddValidItems != null)
                                ValidItemsCount = OrderCreationViewModel.Prescriptions[SelectedRow].AddValidItems.Count;
                            else
                            {
                                SetNotAvailableMenuItem();
                                return;
                            }

                            if (ValidItemsCount > 0)
                            {
                                for (int i = 0; i < ValidItemsCount; i++)
                                {
                                    MenuItem menuItem = new MenuItem() { Header = OrderCreationViewModel.Prescriptions[SelectedRow].AddValidItems[i] };
                                    menuItem.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                                    WVA_OrdersContextMenu.Items.Add(menuItem);
                                }
                            }
                            else
                                throw new Exception("No Valid Items");
                            break;
                        case 11: // If column is a 'Color'
                            if (OrderCreationViewModel.Prescriptions[SelectedRow].ColorValidItems != null)
                                ValidItemsCount = OrderCreationViewModel.Prescriptions[SelectedRow].ColorValidItems.Count;
                            else
                            {
                                SetNotAvailableMenuItem();
                                return;
                            }

                            if (ValidItemsCount > 0)
                            {
                                for (int i = 0; i < ValidItemsCount; i++)
                                {
                                    MenuItem menuItem = new MenuItem() { Header = OrderCreationViewModel.Prescriptions[SelectedRow].ColorValidItems[i] };
                                    menuItem.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                                    WVA_OrdersContextMenu.Items.Add(menuItem);
                                }
                            }
                            else
                                throw new Exception("No Valid Items");
                            break;
                        case 12: // If column is a 'Multifocal'
                            if (OrderCreationViewModel.Prescriptions[SelectedRow].MultifocalValidItems != null)
                                ValidItemsCount = OrderCreationViewModel.Prescriptions[SelectedRow].MultifocalValidItems.Count;
                            else
                            {
                                SetNotAvailableMenuItem();
                                return;
                            }

                            if (ValidItemsCount > 0)
                            {
                                for (int i = 0; i < ValidItemsCount; i++)
                                {
                                    MenuItem menuItem = new MenuItem() { Header = OrderCreationViewModel.Prescriptions[SelectedRow].MultifocalValidItems[i] };
                                    menuItem.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                                    WVA_OrdersContextMenu.Items.Add(menuItem);
                                }
                            }
                            else
                                throw new Exception("No Valid Items");
                            break;
                    }
                }
                catch
                {
                    SetNotAvailableMenuItem();
                }               
            }
            catch (Exception x)
            {
                AppError.ReportOrWrite(x);
            }
        }  
        
        private void SetNotAvailableMenuItem()
        {
            MenuItem menuItem = new MenuItem() { Header = "Not Available" };
            menuItem.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
            WVA_OrdersContextMenu.Items.Add(menuItem);
        }

        // =======================================================================================================================
        // ================================== CRUD Operations for Order ==========================================================
        // =======================================================================================================================

        // Check the validity of the prescription items and update the datagrid with these changes 
        private bool ItemsAreValid()
        {
            // Left Column
            if (AddresseeTextBox.Visibility == Visibility.Visible)
            {
                if (AddresseeTextBox.Text.Trim() == "")
                {
                    AddresseeLabel.Foreground = new SolidColorBrush(Colors.Red);
                    MessageWindow messageWindow = new MessageWindow("\t'Addressee' cannot be blank!");
                    messageWindow.Show();
                    return false;
                }
                if (AddressTextBox.Text.Trim() == "")
                {
                    AddressLabel.Foreground = new SolidColorBrush(Colors.Red);
                    MessageWindow messageWindow = new MessageWindow("\t'Address' cannot be blank!");
                    messageWindow.Show();
                    return false;
                }
                if (CityTextBox.Text.Trim() == "")
                {
                    CityLabel.Foreground = new SolidColorBrush(Colors.Red);
                    MessageWindow messageWindow = new MessageWindow("\t'City' cannot be blank!");
                    messageWindow.Show();
                    return false;
                }
                if (StateComboBox.Text.Trim() == "")
                {
                    StateLabel.Foreground = new SolidColorBrush(Colors.Red);
                    MessageWindow messageWindow = new MessageWindow("\t'State' cannot be blank!");
                    messageWindow.Show();
                    return false;
                }
                if (ZipTextBox.Text.Trim() == "")
                {
                    ZipLabel.Foreground = new SolidColorBrush(Colors.Red);
                    MessageWindow messageWindow = new MessageWindow("\t'Zip' cannot be blank!");
                    messageWindow.Show();
                    return false;
                }                            
            }

            // Right Column
            if (OrderNameTextBox.Text.Trim() == "")
            {
                OrderNameLabel.Foreground = new SolidColorBrush(Colors.Red);
                MessageWindow messageWindow = new MessageWindow("\t'Order Name' cannot be blank!");
                messageWindow.Show();
                return false;
            }

            if (AccountIDTextBox.Text.Trim() == "")
            {
                AccountIDLabel.Foreground = new SolidColorBrush(Colors.Red);
                MessageWindow messageWindow = new MessageWindow("\t'Account ID' cannot be blank!");
                messageWindow.Show();
                return false;
            }

            if (OrderedByTextBox.Text.Trim() == "")
            {
                OrderedByLabel.Foreground = new SolidColorBrush(Colors.Red);
                MessageWindow messageWindow = new MessageWindow("\t'Ordered By' cannot be blank!");
                messageWindow.Show();
                return false;
            }

            // Make sure user has chosen a match
            IList rows = OrdersDataGrid.Items;

            for (int i = 0; i < OrdersDataGrid.Items.Count; i++)
            {
                Prescription prescription = (Prescription)rows[i];
                if (prescription.ProductImagePath == null)
                {
                    MessageWindow messageWindow = new MessageWindow($"\tMust find a match for product in row {i + 1}!");
                    messageWindow.Show();
                    return false;
                }
            }

            // Validate items
            ValidationResponse validationResponse = Verify();
            List<ValidationDetail> listValidationDetail = validationResponse.Data.Products;
            
            foreach (ValidationDetail validationDetail in listValidationDetail)
            {
                if (!validationDetail._BaseCurve.IsValid)
                {
                    PostInvalidItem("BaseCurve");
                    return false;
                }
                if (!validationDetail._Diameter.IsValid)
                {
                    PostInvalidItem("Diameter");
                    return false;
                }
                if (!validationDetail._Sphere.IsValid)
                {
                    PostInvalidItem("Sphere");
                    return false;
                }
                if (!validationDetail._Cylinder.IsValid)
                {
                    PostInvalidItem("Cylinder");
                    return false;
                }
                if (!validationDetail._Axis.IsValid)
                {
                    PostInvalidItem("Axis");
                    return false;
                }
                if (!validationDetail._Add.IsValid)
                {
                    PostInvalidItem("Add");
                    return false;
                }
                if (!validationDetail._Color.IsValid)
                {
                    PostInvalidItem("Color");
                    return false;
                }
                if (!validationDetail._Multifocal.IsValid)
                {
                    PostInvalidItem("Multifocal");
                    return false;
                }
            }

            return true;
        }

        private void PostInvalidItem(string param)
        {
            MessageWindow messageWindow = new MessageWindow($"A parameter '{param}' in the grid is not valid.");
            messageWindow.Show();
        }

        private ValidationResponse Verify()
        {
            try
            {
                List<ValidationDetail> listValidations = new List<ValidationDetail>();

                IList rows = OrdersDataGrid.Items;

                for (int i = 0; i < OrdersDataGrid.Items.Count; i++)
                {
                    Prescription prescription = (Prescription)rows[i];

                    listValidations.Add(new ValidationDetail()
                    {
                        _PatientName = prescription.Patient,
                        _Eye = prescription.Eye,
                        _Quantity = prescription.Quantity.ToString(),
                        _Description = prescription.Product,
                        _Vendor = "",
                        _Price = "",
                        _ID = new ID() { Value = prescription.ID },
                        _CustomerID = new CustomerID() { Value = AccountIDTextBox.Text },
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
                        Key = UserData.Data.ApiKey,
                        ProductsToValidate = new List<ItemDetail>()
                    }
                };

                foreach (ValidationDetail validationDetail in listValidations)
                {
                    validationWrapper.Request.ProductsToValidate.Add(new ValidationDetail(validationDetail));
                }

                string endpoint = "https://orders.wisvis.com/validations";
                string strValidatedProducts = API.Post(endpoint, validationWrapper);
                ValidationResponse validationResponse = JsonConvert.DeserializeObject<ValidationResponse>(strValidatedProducts);

                if (validationResponse.Status == "FAIL" && validationResponse.Message != "Invalid")
                    throw new Exception($"An error has occurred while validating products. Status: {validationResponse.Status} -- Message: {validationResponse.Message}");

                // Show changes in the datagrid and update ViewModels data to match response
                var prods = validationResponse.Data.Products;
                for (int i = 0; i < prods.Count(); i++)
                {
                    // if product is null or invalid, keep old value, else change it to the new value from prods
                    Prescription prescription = new Prescription()
                    {
                        // These properties don't need to be validated
                        ProductImagePath = OrderCreationViewModel.Prescriptions[i].ProductImagePath,
                        IsChecked = OrderCreationViewModel.Prescriptions[i].IsChecked,
                        FirstName = OrderCreationViewModel.Prescriptions[i].FirstName,
                        LastName = OrderCreationViewModel.Prescriptions[i].LastName,
                        Patient = OrderCreationViewModel.Prescriptions[i].Patient,
                        Eye = OrderCreationViewModel.Prescriptions[i].Eye,
                        Quantity = OrderCreationViewModel.Prescriptions[i].Quantity,
                        Date = OrderCreationViewModel.Prescriptions[i].Date,
                        _CustomerID = OrderCreationViewModel.Prescriptions[i]._CustomerID,
                        IsShipToPatient = OrderCreationViewModel.Prescriptions[i].IsShipToPatient,

                        // If prods[i].Property.Value == null change field to old value, else change to new value
                        CanBeValidated = prods[i].CanBeValidated,
                        Product = prods[i]._Description ?? OrderCreationViewModel.Prescriptions[i].Product,
                        ProductCode = Validator.CheckIfValid(prods[i]._ProductKey) ? prods[i]._ProductKey?.Value : OrderCreationViewModel.Prescriptions[i].ProductCode,

                        // NOTE: to help explain ternary statements below for cell colors
                        // If property is null || is blank && errorMessage is null then cell color = White 
                        // If property isValid then cell color = Green
                        // If property not isValid then cell color = Red
                        BaseCurveValidItems = prods[i]._BaseCurve.ValidItems,
                        BaseCurve = Validator.CheckIfValid(prods[i]._BaseCurve) ? prods[i]._BaseCurve.Value : OrderCreationViewModel.Prescriptions[i].BaseCurve,
                        BaseCurveCellColor = AssignCellColor(prodValue: prods[i]._BaseCurve?.Value?.Trim(), isValid: prods[i]._BaseCurve.IsValid, errorMessage: prods[i]._BaseCurve.ErrorMessage, canBeValidated: prods[i].CanBeValidated),

                        DiameterValidItems = prods[i]._Diameter.ValidItems,
                        Diameter = Validator.CheckIfValid(prods[i]._Diameter) ? prods[i]._Diameter.Value : OrderCreationViewModel.Prescriptions[i].Diameter,
                        DiameterCellColor = AssignCellColor(prodValue: prods[i]._Diameter?.Value?.Trim(), isValid: prods[i]._Diameter.IsValid, errorMessage: prods[i]._Diameter.ErrorMessage, canBeValidated: prods[i].CanBeValidated),

                        SphereValidItems = prods[i]._Sphere.ValidItems,
                        Sphere = Validator.CheckIfValid(prods[i]._Sphere) ? prods[i]._Sphere.Value : OrderCreationViewModel.Prescriptions[i].Sphere,
                        SphereCellColor = AssignCellColor(prodValue: prods[i]._Sphere?.Value?.Trim(), isValid: prods[i]._Sphere.IsValid, errorMessage: prods[i]._Sphere.ErrorMessage, canBeValidated: prods[i].CanBeValidated),

                        CylinderValidItems = prods[i]._Cylinder.ValidItems,
                        Cylinder = Validator.CheckIfValid(prods[i]._Cylinder) ? prods[i]._Cylinder.Value : OrderCreationViewModel.Prescriptions[i].Cylinder,
                        CylinderCellColor = AssignCellColor(prodValue: prods[i]._Cylinder?.Value?.Trim(), isValid: prods[i]._Cylinder.IsValid, errorMessage: prods[i]._Cylinder.ErrorMessage, canBeValidated: prods[i].CanBeValidated),

                        AxisValidItems = prods[i]._Axis.ValidItems,
                        Axis = Validator.CheckIfValid(prods[i]._Axis) ? prods[i]._Axis.Value : OrderCreationViewModel.Prescriptions[i].Axis,
                        AxisCellColor = AssignCellColor(prodValue: prods[i]._Axis?.Value?.Trim(), isValid: prods[i]._Axis.IsValid, errorMessage: prods[i]._Axis.ErrorMessage, canBeValidated: prods[i].CanBeValidated),

                        AddValidItems = prods[i]._Add.ValidItems,
                        Add = Validator.CheckIfValid(prods[i]._Add) ? prods[i]._Add.Value : OrderCreationViewModel.Prescriptions[i].Add,
                        AddCellColor = AssignCellColor(prodValue: prods[i]._Add?.Value?.Trim(), isValid: prods[i]._Add.IsValid, errorMessage: prods[i]._Add.ErrorMessage, canBeValidated: prods[i].CanBeValidated),

                        ColorValidItems = prods[i]._Color.ValidItems,
                        Color = Validator.CheckIfValid(prods[i]._Color) ? prods[i]._Color.Value : OrderCreationViewModel.Prescriptions[i].Color,
                        ColorCellColor = AssignCellColor(prodValue: prods[i]._Color?.Value?.Trim(), isValid: prods[i]._Color.IsValid, errorMessage: prods[i]._Color.ErrorMessage, canBeValidated: prods[i].CanBeValidated),

                        MultifocalValidItems = prods[i]._Multifocal.ValidItems,
                        Multifocal = Validator.CheckIfValid(prods[i]._Multifocal) ? prods[i]._Multifocal.Value : OrderCreationViewModel.Prescriptions[i].Multifocal,
                        MultifocalCellColor = AssignCellColor(prodValue: prods[i]._Multifocal?.Value?.Trim(), isValid: prods[i]._Multifocal.IsValid, errorMessage: prods[i]._Multifocal.ErrorMessage, canBeValidated: prods[i].CanBeValidated),
                    };

                    OrderCreationViewModel.Prescriptions[i] = prescription;
                }

                OrdersDataGrid.Items.Refresh();

                return validationResponse;
            }
            catch (Exception x)
            {
                AppError.ReportOrWrite(x);
                return null;
            }
        }

        private OutOrderWrapper GetCompleteOrder()
        {
            try
            {              
                Order order = OrderCreationViewModel.Order;
             
                if (order == null)
                {
                    order = new Order
                    {
                        Items = new List<Item>()
                    };
                }
                else
                {
                    order.Items?.Clear();
                }
                    
                List<Item> listItems = new List<Item>();
                IList rows = OrdersDataGrid.Items;

                // Set orderID, created date if available
                order.ID = order.ID ?? null;
                order.CreatedDate = order.CreatedDate ?? null;
                order.WvaStoreID = order.WvaStoreID ?? null;

                // Update order object with the form data                  
                order.OrderName         =   OrderNameTextBox.Text;
                order.CustomerID        =   AccountIDTextBox.Text;
                order.OrderedBy         =   OrderedByTextBox.Text;
                //order.OfficeName        =   OfficeNameTextBox.Text;
                order.PoNumber          =   PoNumberTextBox.Text;
                order.ShippingMethod    =   GetShippingTypeID(ShippingTypeComboBox.Text);

                try   { order.ShipToPatient = OrderCreationViewModel.Prescriptions[0].IsShipToPatient ? "Y" : "N"; }
                catch { order.ShipToPatient = ""; }

                if (StateComboBox.Visibility == Visibility.Visible)
                {                   
                    order.Name_1        =   AddresseeTextBox.Text;
                    order.StreetAddr_1  =   AddressTextBox.Text;
                    order.StreetAddr_2  =   Suite_AptTextBox.Text;
                    order.State         =   StateComboBox.Text.Length > 2 ? StateComboBox.Text.Substring(StateComboBox.Text.Length - 2) : StateComboBox.Text;
                    order.City          =   CityTextBox.Text;
                    order.Zip           =   ZipTextBox.Text;
                    order.Phone         =   PhoneTextBox.Text;
                    order.DoB           =   DoBTextBox.Text;
                }
                              
                // Update order detail                
                for (int i = 0; i < OrdersDataGrid.Items.Count; i++)
                {
                    Prescription prescription = (Prescription)rows[i];

                     order.Items.Add(new Item()
                    {                            
                        ID              =   Guid.NewGuid().ToString().Replace("-", ""),    
                        FirstName       =   prescription.FirstName,
                        LastName        =   prescription.LastName,
                        PatientID       =   prescription._CustomerID?.Value,
                        Eye             =   prescription.Eye,
                        Quantity        =   prescription.Quantity == null || prescription.Quantity.Trim() == "" || prescription.Quantity == "0" ? "1" : prescription.Quantity,
                        ItemRetailPrice =   prescription.Price,
                        ProductDetail   =   new OrderDetail()
                        {
                            Name             =    prescription.Product,
                            ProductReviewed  =    prescription.ProductImagePath == null || prescription.ProductImagePath == "" ? false : true,
                            SKU              =    prescription.SKU,
                            ProductKey       =    prescription.ProductCode,
                            UPC              =    prescription.UPC,
                            BaseCurve        =    prescription.BaseCurve,
                            Diameter         =    prescription.Diameter,
                            Sphere           =    prescription.Sphere,
                            Cylinder         =    prescription.Cylinder,
                            Axis             =    prescription.Axis,
                            Add              =    prescription.Add,
                            Color            =    prescription.Color,
                            Multifocal       =    prescription.Multifocal,
                        }
                    });
                }

                // Create the complete order object 
                OutOrderWrapper outOrderWrapper = new OutOrderWrapper()
                {
                    OutOrder = new OutOrder()
                    {
                        ApiKey = UserData.Data.ApiKey,
                        PatientOrder = order
                    }
                };

                return outOrderWrapper;
            }
            catch (Exception x)
            {
                AppError.ReportOrWrite(x);
                return null;
            }           
        }

        private void SaveOrder(OutOrderWrapper outOrderWrapper)
        {
            try
            {
                Response response = OrderCreationViewModel.SaveOrder(outOrderWrapper);

                if (response.Status == "SUCCESS")
                {
                    new MessageWindow("\t\tOrder Saved!").Show();
                }
                else
                    throw new Exception($"Save order failed. Error message: {response.Message}");                   
            }
            catch (Exception x)
            {
                AppError.ReportOrWrite(x);
            }           
        }

        private void DeleteOrder()
        {
            Response response = OrderCreationViewModel.DeleteOrder(OrderNameTextBox.Text);

            if (response.Status == "SUCCESS")
            {
                ClearView();

                new MessageWindow("\t\tOrder deleted!").Show();

                // Change view to WVA Orders view
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                        (window as MainWindow).MainContentControl.DataContext = new OrdersView(null, null, "CompulinkOrders");
                }
            }
            else
            {
                MessageBox.Show("An error has occurred. Order not deleted.", "", MessageBoxButton.OK);
            }
        }       

        private void CreateOrder()
        {
            try
            {
                // Get the completed order object
                OutOrderWrapper outOrderWrapper = GetCompleteOrder();

                if (outOrderWrapper == null)
                {
                    new MessageWindow("Please be sure all items are valid before submitting.").Show();
                    return;
                }
                
               OrderResponse response = OrderCreationViewModel.CreateOrder(outOrderWrapper);
                                       
                if (response == null)
                    throw new Exception("Null response from api on order creation.");

                // Check response and handle accordingly
                if (response.Status == "SUCCESS")
                {
                    ClearView();

                    string message = "";
                    if (response?.Data?.Wva_order_id != null)
                        message = $"Order created! WVA Order ID:{response.Data.Wva_order_id}";
                    else
                        message = "\t\tOrder created!";

                    new MessageWindow(message).Show();

                    // Change view to WVA Orders view
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                            (window as MainWindow).MainContentControl.DataContext = new OrdersView(null, null, "WVAOrders");
                    }
                }
                else
                {
                    new MessageWindow($"Order creation has failed with the following message: {response.Message}").Show();
                }                         
            }
            catch (Exception x)
            {
                new MessageWindow("Order creation failed. Please see error log for details.").Show();
                AppError.ReportOrWrite(x);
            }
        }

        private void AutoFillParameterCells(int row)
        {
            if (OrderCreationViewModel.Prescriptions.Count > 0)
            {
                FindProductMatches();
                SetMenuItems();
                Verify();

                if (OrderCreationViewModel.Prescriptions[row]?.BaseCurveValidItems?.Count == 1 && (OrderCreationViewModel.Prescriptions[row].BaseCurve == null || OrderCreationViewModel.Prescriptions[row].BaseCurve.Trim() == ""))
                {
                    OrdersDataGrid.GetCell(row, 5).Content = OrderCreationViewModel.Prescriptions[row]?.BaseCurveValidItems[0];
                    OrderCreationViewModel.Prescriptions[row].BaseCurve = OrderCreationViewModel.Prescriptions[row]?.BaseCurveValidItems[0];
                    Verify();
                }

                if (OrderCreationViewModel.Prescriptions[row]?.DiameterValidItems?.Count == 1)
                {
                    OrdersDataGrid.GetCell(row, 6).Content = OrderCreationViewModel.Prescriptions[row].DiameterValidItems[0];
                    OrderCreationViewModel.Prescriptions[row].Diameter = OrderCreationViewModel.Prescriptions[row].DiameterValidItems[0];
                    Verify();
                }

                if (OrderCreationViewModel.Prescriptions[row]?.SphereValidItems?.Count == 1)
                {
                    OrdersDataGrid.GetCell(row, 7).Content = OrderCreationViewModel.Prescriptions[row].SphereValidItems[0];
                    OrderCreationViewModel.Prescriptions[row].Sphere = OrderCreationViewModel.Prescriptions[row].SphereValidItems[0];
                    Verify();
                }

                if (OrderCreationViewModel.Prescriptions[row]?.CylinderValidItems?.Count == 1)
                {
                    OrdersDataGrid.GetCell(row, 8).Content = OrderCreationViewModel.Prescriptions[row].CylinderValidItems[0];
                    OrderCreationViewModel.Prescriptions[row].Cylinder = OrderCreationViewModel.Prescriptions[row].CylinderValidItems[0];
                    Verify();
                }

                if (OrderCreationViewModel.Prescriptions[row]?.AxisValidItems?.Count == 1)
                {
                    OrdersDataGrid.GetCell(row, 9).Content = OrderCreationViewModel.Prescriptions[row].AxisValidItems[0];
                    OrderCreationViewModel.Prescriptions[row].Axis = OrderCreationViewModel.Prescriptions[row].AxisValidItems[0];
                    Verify();
                }

                if (OrderCreationViewModel.Prescriptions[row]?.AddValidItems?.Count == 1)
                {
                    OrdersDataGrid.GetCell(row, 10).Content = OrderCreationViewModel.Prescriptions[row].AddValidItems[0];
                    OrderCreationViewModel.Prescriptions[row].Add = OrderCreationViewModel.Prescriptions[row].AddValidItems[0];
                    Verify();
                }

                if (OrderCreationViewModel.Prescriptions[row]?.ColorValidItems?.Count == 1)
                {
                    OrdersDataGrid.GetCell(row, 11).Content = OrderCreationViewModel.Prescriptions[row].ColorValidItems[0];
                    OrderCreationViewModel.Prescriptions[row].Color = OrderCreationViewModel.Prescriptions[row].ColorValidItems[0];
                    Verify();
                }

                if (OrderCreationViewModel.Prescriptions[row]?.MultifocalValidItems?.Count == 1)
                {
                    OrdersDataGrid.GetCell(row, 12).Content = OrderCreationViewModel.Prescriptions[row].MultifocalValidItems[0];
                    OrderCreationViewModel.Prescriptions[row].Multifocal = OrderCreationViewModel.Prescriptions[row].MultifocalValidItems[0];
                    Verify();
                }
            }
        }

        // =======================================================================================================================
        // ================================== Event Handlers =====================================================================
        // =======================================================================================================================

        private void WVA_OrdersContextMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                MenuItem menuItem = sender as MenuItem;
                string selectedItem = menuItem.Header.ToString();

                // Drop out of function and populate context menu with a lower match tolerance
                if (selectedItem.Contains("More Matches"))
                {
                    // Run match FindMatch again with higher tolerances
                    FindProductMatches(60, true);
                    SetMenuItems();
                    WVA_OrdersContextMenu.IsOpen = true;
                    return;
                }

                // Get selected row and column
                int row = OrdersDataGrid.Items.IndexOf(OrdersDataGrid.CurrentItem);
                int column = OrdersDataGrid.CurrentColumn.DisplayIndex;

                // Only want to change 'Products' column 
                if (selectedItem != "No Matches Found" && selectedItem != "Not Available")
                    AutoFillParameterCells(row);
                    
                OrdersDataGrid.GetCell(row, column).Content = selectedItem;

                if (column <= 4)
                {
                    string compulinkProduct = (OrdersDataGrid.CurrentItem as Prescription).Product;
                    OrderCreationViewModel.Prescriptions[row].Product = selectedItem;
                    OrderCreationViewModel.Prescriptions[row].ProductImagePath = @"/Resources/CheckMarkCircle.png";
                    ProductPrediction.LearnProduct(compulinkProduct, selectedItem);
                }
                if (column == 5)
                    OrderCreationViewModel.Prescriptions[row].BaseCurve = selectedItem;
                if (column == 6)
                    OrderCreationViewModel.Prescriptions[row].Diameter = selectedItem;
                if (column == 7)
                    OrderCreationViewModel.Prescriptions[row].Sphere = selectedItem;
                if (column == 8)
                    OrderCreationViewModel.Prescriptions[row].Cylinder = selectedItem;
                if (column == 9)
                    OrderCreationViewModel.Prescriptions[row].Axis = selectedItem;
                if (column == 10)
                    OrderCreationViewModel.Prescriptions[row].Add = selectedItem;
                if (column == 11)
                    OrderCreationViewModel.Prescriptions[row].Color = selectedItem;
                if (column == 12)
                    OrderCreationViewModel.Prescriptions[row].Multifocal = selectedItem;

                AutoFillParameterCells(row);
                Verify();

                Mouse.OverrideCursor = Cursors.Arrow;
            }
            catch (Exception x)
            {
                AppError.ReportOrWrite(x);
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        private void OrdersDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
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
                AppError.ReportOrWrite(x);
            }
        }

        private void OrdersDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {                
                FindProductMatches();               
                SetMenuItems();
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
            }          
        }

        private void MinScoreAdjustSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            try
            {
                MatchPercentLabel.Content = $"Match Percent: {Convert.ToInt16(MinScoreAdjustSlider.Value)}%";
                if (WVA_OrdersContextMenu.Items.Count > 0)
                {               
                    FindProductMatches();
                    SetMenuItems();
                }
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
            }
        }

        private void VerifyOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FindProductMatches();
                SetMenuItems();
                Verify();
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
            }           
        }

        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this order?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result.ToString() == "Yes")
            {
                DeleteOrder();
            }
        }

        private void SubmitOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {                            
                if (ItemsAreValid())
                    CreateOrder();                      
            }
            catch (Exception x)
            {
                new MessageWindow("An error has occurred Please see error log for details.").Show();
                AppError.ReportOrWrite(x);
            }
        }

        // Save order to database
        private void SaveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OutOrderWrapper outOrderWrapper = GetCompleteOrder();

                if (outOrderWrapper != null)
                    SaveOrder(outOrderWrapper);
                else
                    throw new NullReferenceException();             
            }
            catch (Exception x)
            {
                AppError.ReportOrWrite(x);
            }           
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            OutOrderWrapper outOrderWrapper = GetCompleteOrder();
            OrderCreationViewModel.SaveOrder(outOrderWrapper);
        }

        private void OrderNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            OrderNameLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void AccountIDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AccountIDLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void OrderedByTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            OrderedByLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void AddresseeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddresseeLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void AddressTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddressLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void CityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CityLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void StateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StateLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void ZipTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ZipLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PhoneLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void DoBTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DoBLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //AutosaveOrder();
        }
    }
}

