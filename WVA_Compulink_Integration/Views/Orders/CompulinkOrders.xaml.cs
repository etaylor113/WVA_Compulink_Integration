using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Models.Order.Out;
using WVA_Compulink_Integration.Models.Patient;
using WVA_Compulink_Integration.Models.Prescription;
using WVA_Compulink_Integration.Utility.File;
using WVA_Compulink_Integration.ViewModels;
using WVA_Compulink_Integration.ViewModels.Orders;
using WVA_Compulink_Integration.Views.Search;

namespace WVA_Compulink_Integration.Views.Orders
{
    /// <summary>
    /// Interaction logic for CompulinkOrders.xaml
    /// </summary>
    public partial class CompulinkOrders : UserControl
    {
        ToolTip toolTip = new ToolTip();

        public CompulinkOrders()
        {
            InitializeComponent();
            SetUp();
        }

        // Do any setup after loading the view
        private void SetUp()
        {      
            IsVisibleChanged += new DependencyPropertyChangedEventHandler(LoginControl_IsVisibleChanged);
            SetUpOrdersDataGrid();
            GetWvaOrders();            
        }
      
        // Get wva orders for this user 
        private void GetWvaOrders()
        {
            try
            {
                // Autofill new order display
                WvaOrdersComboBox.Text = $"WVA Order {DateTime.Now.ToString("MM/dd/yy--HH:mm:ss")}";

                // Get this account's open wva orders
                string endpoint = "http://localhost:56075/CompuClient/orders/get-names/" +  UserData._User?.Account;
                string strNames = API.Get(endpoint, out string httpStatus);
                Dictionary<string, string> dictOrderNames = JsonConvert.DeserializeObject<Dictionary<string, string>>(strNames);

                // Break if bad response
                if (httpStatus != "OK")
                    throw new Exception("Bad response from server!");
             
                // Put account's open orders in the drop down
                if (dictOrderNames.Count > 0)
                {
                    foreach (string orderName in dictOrderNames.Values)
                        WvaOrdersComboBox.Items.Add(orderName);            
                }
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
        }

        // Filters out orders in table by whatever the user enters in the search text box
        private void SearchOrders(string searchString)
        {
            try
            {
                OrdersDataGrid.Items.Clear();

                List<Prescription> tempList = Memory.Orders.CompulinkOrders.Where(x => x.Patient.ToLower().Replace(",","").StartsWith(searchString.ToLower().Replace(",", ""))).ToList();
                       
                foreach (Prescription prescription in tempList)
                {
                    OrdersDataGrid.Items.Add(prescription);
                }
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
        }

        // Asyncronously get Compulink Orders from server
        private async Task<List<Prescription>> GetCompulinkOrders()
        {           
            string endpoint = "http://localhost:56075/CompuClient/prescriptions/";
            string strPrescriptions = API.Get(endpoint, out string httpStatus);

            if (strPrescriptions == null)
                throw new NullReferenceException();

            return JsonConvert.DeserializeObject<List<Prescription>>(strPrescriptions);           
        }

        // Set up items in OrdersDataGrid 
        private async void SetUpOrdersDataGrid()
        {
            try
            {
                OrdersDataGrid.Items.Clear();
                Memory.Orders.CompulinkOrders.Clear();

                var prescriptions = await GetCompulinkOrders();

                foreach (Prescription prescription in prescriptions)
                {
                    OrdersDataGrid.Items.Add(prescription);
                    Memory.Orders.CompulinkOrders.Add(prescription);
                }
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
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

                // Add only checked orders to list
                foreach (Prescription prescription in rows)
                {
                    if (prescription.IsChecked)
                    {                       
                        listPrescriptions.Add(prescription);
                        totalCount++;
                        if (prescription.IsShipToPatient)
                            stpCount++;
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
                            (window as MainWindow).MainContentControl.DataContext = new OrdersView(listPrescriptions, WvaOrdersComboBox.Text,  "OrderCreation");                    
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
