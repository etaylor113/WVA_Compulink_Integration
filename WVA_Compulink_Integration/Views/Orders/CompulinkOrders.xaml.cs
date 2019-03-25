using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Models.Prescription;
using WVA_Compulink_Integration.Views.Search;

namespace WVA_Compulink_Integration.Views.Orders
{
    /// <summary>
    /// Interaction logic for CompulinkOrders.xaml
    /// </summary>
    public partial class CompulinkOrders : UserControl
    {
        ToolTip toolTip = new ToolTip();
        List<Prescription> listPrescriptions = new List<Prescription>();

        public CompulinkOrders()
        {
            InitializeComponent();
            SetUp();
        }

        private void SetUp()
        {
            //OrdersDataGrid.ItemsSource = Memory.Orders.CompulinkOrders;
            IsVisibleChanged += new DependencyPropertyChangedEventHandler(LoginControl_IsVisibleChanged);
            SetUpOrdersDataGrid();
            GetWvaOrders();            
        }

        // Set up items in OrdersDataGrid 
        private async void SetUpOrdersDataGrid()
        {
            try
            {
                Memory.Orders.CompulinkOrders.Clear();
                listPrescriptions.Clear();

                var prescriptionWrapper = await GetCompulinkOrders();

                if (prescriptionWrapper == null)
                    throw new NullReferenceException("Response returned null from endpoint while getting Compulink orders.");
                   
                var products = prescriptionWrapper?.Request?.Products;

                foreach (Prescription prescription in products)
                {
                    var presc = prescription;

                    // Clean up Compulink data
                    presc.BaseCurve = prescription.BaseCurve.Trim().Replace(" ", "");
                    presc.Diameter = prescription.Diameter.Trim().Replace(" ", "");
                    presc.Add = prescription.Add.Trim().Replace(" ", "");
                    presc.Axis = prescription.Axis.Trim().Replace(" ", "");
                    presc.Multifocal = prescription.Multifocal.Trim().Replace(" ", "");
                    presc.Sphere = prescription.Sphere.Trim().Replace(" ", "");
                    presc.Cylinder = prescription.Cylinder.Trim().Replace(" ", "");

                    Memory.Orders.CompulinkOrders.Add(presc);
                }

                listPrescriptions.AddRange(Memory.Orders.CompulinkOrders);
                OrdersDataGrid.ItemsSource = listPrescriptions;
                OrdersDataGrid.Items.Refresh();
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
        }

        // Get wva orders for this user 
        private void GetWvaOrders()
        {
            try
            {
                // Autofill new order display
                WvaOrdersComboBox.Text = RemoveUnsafeChars($"[{UserData.Data.UserName}]s order {DateTime.Now.ToString("MM/dd/yy--HH:mm:ss")}");
                                        
                // Get this account's open wva orders
                string dsn = UserData.Data.DSN;    
                string endpoint = $"http://{dsn}/api/order/get-names/" + UserData.Data?.Account;
                string strNames = API.Get(endpoint, out string httpStatus);

                if (strNames == null || strNames.ToString().Trim() == "")
                    throw new NullReferenceException("Response null ");

                // Break if bad response
                if (httpStatus != "OK")
                    throw new Exception($"Http status: {httpStatus.ToString()} from server while getting order names!");

                Dictionary<string, string> dictOrderNames = JsonConvert.DeserializeObject<Dictionary<string, string>>(strNames);
             
                // Put account's open orders in the drop down
                if (dictOrderNames?.Count > 0)
                {
                    foreach (string orderName in dictOrderNames?.Values)
                        WvaOrdersComboBox.Items.Add(orderName);            
                }
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
        }

        // Asyncronously get Compulink Orders from server
        private async Task<PrescriptionWrapper> GetCompulinkOrders()
        {
            try
            {
                string dsn = UserData.Data.DSN;
                string actNum = UserData.Data.Account;
                string endpoint = $"http://{dsn}/api/openorder/{actNum}";
                string strPrescriptions = API.Get(endpoint, out string httpStatus);

                if (strPrescriptions == null || strPrescriptions.Trim() == "")
                    throw new NullReferenceException("Response from open orders is null or empty.");

                // Break if bad response
                if (httpStatus != "OK")
                    throw new Exception($"Http status: {httpStatus.ToString()} from server while getting order names!");

                PrescriptionWrapper prescriptionWrapper = JsonConvert.DeserializeObject<PrescriptionWrapper>(strPrescriptions);


                // Remove any unsafe sql characters from compulink data
                for (int i = 0; i < prescriptionWrapper.Request.Products.Count; i++)
                {
                    var p = prescriptionWrapper.Request.Products[i];

                    p._CustomerID.Value  = RemoveUnsafeChars(p._CustomerID.Value);
                    p.Patient            = RemoveUnsafeChars(p.Patient);
                    p.Add                = RemoveUnsafeChars(p.Add);
                    p.BaseCurve          = RemoveUnsafeChars(p.BaseCurve);
                    p.Color              = RemoveUnsafeChars(p.Color);
                    p.Cylinder           = RemoveUnsafeChars(p.Cylinder);
                    p.Axis               = RemoveUnsafeChars(p.Axis);
                    p.Diameter           = RemoveUnsafeChars(p.Diameter);
                    p.Eye                = RemoveUnsafeChars(p.Eye);
                    p.FirstName          = RemoveUnsafeChars(p.FirstName);
                    p.LastName           = RemoveUnsafeChars(p.LastName);
                    p.Multifocal         = RemoveUnsafeChars(p.Multifocal);
                    p.Sphere             = RemoveUnsafeChars(p.Sphere);
                }

                return prescriptionWrapper;
            }
            catch (Exception ex)
            {
                AppError.PrintToLog(ex);
                return null;
            }
        }

        // Filters out orders in table by whatever patient they search for in the searc box
        private void SearchOrders(string searchString)
        {
            try
            {
                listPrescriptions.Clear();

                List<Prescription> tempList = Memory.Orders.CompulinkOrders.Where(x => x.Patient.ToLower().Replace(",","").StartsWith(searchString.ToLower().Replace(",", ""))).ToList();
                       
                foreach (Prescription prescription in tempList)
                {
                    listPrescriptions.Add(prescription);
                }

                OrdersDataGrid.Items.Refresh();
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
        }
        private string RemoveUnsafeChars(string originalString)
        {
            return originalString.Replace("<", "")
                                 .Replace(">", "")
                                 .Replace("'", "")
                                 .Replace("\"", "")
                                 .Replace("|", "")
                                 .Replace(";", "")
                                 .Replace("\\", "")
                                 .Replace("~", "")
                                 .Replace("{", "")
                                 .Replace("}", "")
                                 .Replace("[", "")
                                 .Replace("]", "")
                                 .Replace("%", "")
                                 .Replace("*", "")
                                 .Replace("=", "")
                                 .Replace("^", "")
                                 .Replace("$", "")
                                 .Replace("+", "")
                                 .Replace("?", "")
                                 .Replace("&", "");
        }

        // Allow SearchTextBox to get focus
        void LoginControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                Dispatcher.BeginInvoke(
                DispatcherPriority.ContextIdle,
                new Action(delegate ()
                {
                    SearchTextBox.Focus();
                }));
            }
        }

        // Search Text Box 
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchOrders(SearchTextBox.Text);
        }

        // Refresh Button
        private void RefreshButton_MouseEnter(object sender, MouseEventArgs e)
        {           
            // Open tool tip on mouseover, and change image color
            toolTip.Content = "Refresh Content";
            toolTip.IsOpen = true;
            RefreshImage.Source = new BitmapImage(new Uri(@"/Resources/icons8-available-updates-48.png", UriKind.Relative));
        }

        private void RefreshButton_MouseLeave(object sender, MouseEventArgs e)
        {
            // Close tool tip on mouseleave, and change image color
            toolTip.IsOpen = false;
            RefreshImage.Source = new BitmapImage(new Uri(@"/Resources/icons8-available-updates-filled-48.png", UriKind.Relative));            
        }

        // Get data from compulink and refresh table data
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {          
            // Spawn a loading window and change cursor to waiting cursor
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            Mouse.OverrideCursor = Cursors.Wait;

            SearchTextBox.Text = "";
            SetUpOrdersDataGrid();

            // Close loading window and change cursor back to default arrow cursor
            loadingWindow.Close();
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        // Create Order Button
        private void AddToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get order name, selected row in table, and patient object attatched to row
                IList rows = OrdersDataGrid.Items;

                int stpCount = 0;
                int totalCount = 0;
                List<Prescription> listPrescriptions = new List<Prescription>();
                List<string> listSTP = new List<string>();

                // Add only checked orders to list
                foreach (Prescription prescription in rows)
                {
                    if (prescription.IsChecked)
                    {
                        listPrescriptions.Add(prescription);
                        totalCount++;
                        if (prescription.IsShipToPatient)
                        {
                            stpCount++;
                            listSTP.Add(prescription.Patient);
                        }
                    }
                }

                // Make sure user can't add more than 2 different patients to STP order
                if (listSTP.Count > 2)
                {
                    MessageBox.Show("Cannot add multiple patients to a STP order!", "Compulink Integration", MessageBoxButton.OK);
                    return;
                }

                foreach (string patient in listSTP)
                {
                    if (listSTP.Count > 1)
                    {
                        List<Prescription> prescriptions = listPrescriptions.FindAll(x => x.Patient == patient);

                        if (prescriptions.Count != 2)
                        {
                            MessageBox.Show("Cannot add different patients to a STP order!", "Compulink Integration", MessageBoxButton.OK);
                            return;
                        }
                    }                 
                }                             

                // Make sure user can't create a mixed STP & STO order
                if (stpCount > 0 && stpCount != totalCount)
                {
                    // Notify user they can't create a mixed order
                    MessageBox.Show("Cannot create a mixed Ship to Patient/Ship to Office order!", "Compulink Integration", MessageBoxButton.OK);
                }                  
                else if (totalCount == 0)
                {
                    // Notify user they must select at least one Compulink order
                    MessageBox.Show("You must select at least one Compulink order. \nClick checkbox in the (+) column to add it to the order.", "Compulink Integration", MessageBoxButton.OK);
                }
                else
                {                              
                    // Jumps back to parent view and changes it to order creation view 
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            (window as MainWindow).MainContentControl.DataContext = new OrdersView(listPrescriptions, RemoveUnsafeChars(WvaOrdersComboBox.Text),  "OrderCreation");                    
                            return;
                        }
                    }
                }                
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
        }

    }
}
