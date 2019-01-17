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
            List<Order> listWvaOrders = Memory.Orders.WVAOrders;
            
            try { WvaOrdersComboBox.Text = $"wva_order-{File.ReadAllText(Paths.ActNumFile)}-{DateTime.Now.ToString("yyyy-MM-dd")}"; }
            catch { WvaOrdersComboBox.Text = $"wva_order-{DateTime.Now.ToString("yyyy-MM-dd")}"; }

          
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
        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get order name, selected row in table, and patient object attatched to row
                IList rows = OrdersDataGrid.SelectedItems;
                Patient patient = (Patient)rows[0];

                // Jumps back to parent view and changes it to order creation view 
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).MainContentControl.DataContext = new OrderCreationViewModel(patient);
                        return;
                    }
                }
            }
            catch
            {

            }
        }


    }
}
