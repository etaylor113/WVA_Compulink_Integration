using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using WVA_Compulink_Integration.Views.Search;

namespace WVA_Compulink_Integration.Views.Orders
{
    /// <summary>
    /// Interaction logic for WVAOrders.xaml
    /// </summary>
    public partial class WVAOrders : UserControl
    {
        ToolTip toolTip = new ToolTip();

        public WVAOrders()
        {
            InitializeComponent();
            SetUp();
        }

        // Do any setup after loading the view
        private void SetUp()
        {
            IsVisibleChanged += new DependencyPropertyChangedEventHandler(LoginControl_IsVisibleChanged);
            SetUpOrdersDataGrid();
        }

        private void SearchOrders(int index, string searchString)
        {
            try
            {
                OrdersDataGrid.Items.Clear();

                if (searchString == "")
                    return;

                List<Patient> tempList = new List<Patient>();

                switch (index)
                {
                    // PatientID
                    case 0:
                        tempList = ListPatients.OrigListPatients.Where(x => x.PatientID.ToLower().StartsWith(searchString.ToLower().Replace(",", ""))).ToList();
                        break;
                    // Name
                    case 1:
                        tempList = ListPatients.OrigListPatients.Where(x => x.FullName.ToLower().StartsWith(searchString.ToLower().Replace(",", ""))).ToList();
                        break;
                    // Street
                    case 2:
                        tempList = ListPatients.OrigListPatients.Where(x => x.Street.ToLower().StartsWith(searchString.ToLower().Replace(",", ""))).ToList();
                        break;
                    // City
                    case 3:
                        tempList = ListPatients.OrigListPatients.Where(x => x.City.ToLower().StartsWith(searchString.ToLower().Replace(",", ""))).ToList();
                        break;
                    // State
                    case 4:
                        tempList = ListPatients.OrigListPatients.Where(x => x.State.ToLower().StartsWith(searchString.ToLower().Replace(",", ""))).ToList();
                        break;
                    // Zip
                    case 5:
                        tempList = ListPatients.OrigListPatients.Where(x => x.Zip.ToLower().StartsWith(searchString.ToLower().Replace(",", ""))).ToList();
                        break;
                    // Phone
                    case 6:
                        tempList = ListPatients.OrigListPatients.Where(x => x.Phone.StartsWith(searchString.ToLower().Replace(",", ""))).ToList();
                        break;
                    default:
                        break;
                }

                foreach (Patient patient in tempList)
                    OrdersDataGrid.Items.Add(patient);
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
        }

        // Asyncronously return this account's orders from the server 
        private async Task<List<Order>> GetWVAOrders()
        {       
            string endpoint = "http://localhost:56075/CompuClient/prescriptions/";
            string strORders = API.Get(endpoint, out string httpStatus);

            if (strORders == null)
                throw new NullReferenceException();

            return JsonConvert.DeserializeObject<List<Order>>(strORders);
           
        }

        private async void SetUpOrdersDataGrid()
        {
            try
            {
              
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
            SearchOrders(SearchFilterComboBox.SelectedIndex, SearchTextBox.Text);
        }

        // Refresh Button
        private void RefreshButton_MouseEnter(object sender, MouseEventArgs e)
        {
            toolTip.Content = "Refresh Content";
            toolTip.IsOpen = true;
            RefreshImage.Source = new BitmapImage(new Uri(@"/Resources/icons8-available-updates-48.png", UriKind.Relative));
        }

        private void RefreshButton_MouseLeave(object sender, MouseEventArgs e)
        {
            toolTip.IsOpen = false;
            RefreshImage.Source = new BitmapImage(new Uri(@"/Resources/icons8-available-updates-filled-48.png", UriKind.Relative));
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            // Spawn a loading window and change cursor to waiting cursor
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            Mouse.OverrideCursor = Cursors.Wait;

            SetUpOrdersDataGrid();

            // Close loading window and change cursor back to default arrow cursor
            loadingWindow.Close();
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        // Submit Button
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Spawn a loading window and change cursor to waiting cursor
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            Mouse.OverrideCursor = Cursors.Wait;

            //await Task.Run(() => List_WVA_Products.LoadProducts());

            // Close loading window and change cursor back to default arrow cursor
            loadingWindow.Close();
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        // Edit Button
        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {

        }

        // Delete Button
        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
