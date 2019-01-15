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
using System.Windows.Threading;
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Models.Patient;
using WVA_Compulink_Integration.ViewModels;
using WVA_Compulink_Integration.ViewModels.Search;

namespace WVA_Compulink_Integration.Views.Search
{
    /// <summary>
    /// Interaction logic for SearchPatientsView.xaml
    /// </summary>
   
    public partial class SearchPatientsView : UserControl
    {
        public SearchPatientsView()
        {       
            InitializeComponent();
            RunSearchViewSetup();
        }

        private void RunSearchViewSetup()
        {
            ResetUI();
            SetUpPatientDataGrid();
            HideOrShowDataGrid();
            IsVisibleChanged += new DependencyPropertyChangedEventHandler(LoginControl_IsVisibleChanged);
        }

        private void ResetUI()
        {
            SearchTextBox.Clear();
            PatientDataGrid.Items.Clear();           
        }

        private void HideOrShowDataGrid()
        {         
            if (PatientDataGrid.Items.Count == 0)
            {
                // Hide datagrid 

                PatientDataGrid.Visibility = Visibility.Hidden;
                
                // hide no results label if they haven't searched for anything yet
                if (SearchTextBox.Text == "")             
                    NoResultsLabel.Visibility = Visibility.Hidden;   
                else
                    NoResultsLabel.Visibility = Visibility.Visible;
            }
            else
            {
                // Show datagrid and hide no results label
                PatientDataGrid.Visibility = Visibility.Visible;
                NoResultsLabel.Visibility = Visibility.Hidden;
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

        /// <summary>
        /// Setup PatientDataGrid
        /// </summary>

        private async void SetUpPatientDataGrid()
        {
            // Spawn a loading window and change cursor to waiting cursor
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            Mouse.OverrideCursor = Cursors.Wait;
        
            // Get patient data
            List<Patient> listPatients = await GetPatientData();

            // Input DataGrid data
            LoadDataGrid(listPatients);

            // Close loading window and change cursor back to default arrow cursor
            loadingWindow.Close();
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private async Task<List<Patient>> GetPatientData()
        {
            string strPatients = await Task.Run(() => SearchPatientsViewModel.GetPatientsAsync());
            return JsonConvert.DeserializeObject<List<Patient>>(strPatients);
        }

        private void LoadDataGrid(List<Patient> listPatients)
        {
            foreach (Patient patient in listPatients)
            {
                ListPatients.OrigListPatients.Add(patient);
                PatientDataGrid.Items.Add(patient);
            }
        }
      
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ResetUI();
            SetUpPatientDataGrid();
        }

        /// <summary>
        /// Searching Functions
        /// </summary>
        
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {          
            SearchPatients(PatientFilterComboBox.SelectedIndex, SearchTextBox.Text);
            HideOrShowDataGrid();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {      
            // Run search function without quick search. Better performance for high number of columns
            SearchPatients(PatientFilterComboBox.SelectedIndex, SearchTextBox.Text);
        }  

        private void SearchPatients(int index, string searchString)
        {
            try
            {
                PatientDataGrid.Items.Clear();

                if (searchString == "")             
                    return;

                List<Patient> tempList = new List<Patient>();
           
                switch (index)
                {
                    // PatientID
                    case 0:
                        tempList = ListPatients.OrigListPatients.Where(x => x.PatientID.StartsWith(searchString.ToLower().Replace(",", ""))).ToList();
                        break;
                    // Name
                    case 1:
                        tempList = ListPatients.OrigListPatients.Where(x => x.FullName.ToLower().StartsWith(searchString.ToLower().Replace(",",""))).ToList();                                            
                        break;
                    // Street
                    case 2:
                        tempList = ListPatients.OrigListPatients.Where(x => x.Street.StartsWith(searchString.ToLower().Replace(",", ""))).ToList();
                        break;
                    // City
                    case 3:
                        tempList = ListPatients.OrigListPatients.Where(x => x.City.StartsWith(searchString.ToLower().Replace(",", ""))).ToList();
                        break;
                    // State
                    case 4:
                        tempList = ListPatients.OrigListPatients.Where(x => x.State.StartsWith(searchString.ToLower().Replace(",", ""))).ToList();
                        break;
                    // Zip
                    case 5:
                        tempList = ListPatients.OrigListPatients.Where(x => x.Zip.StartsWith(searchString.ToLower().Replace(",", ""))).ToList();
                        break;
                    // Phone
                    case 6:
                        tempList = ListPatients.OrigListPatients.Where(x => x.Phone.StartsWith(searchString.ToLower().Replace(",", ""))).ToList();
                        break;
                    default:
                        break;
                }

                foreach (Patient patient in tempList)
                    PatientDataGrid.Items.Add(patient);
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IList rows = PatientDataGrid.SelectedItems;
            Patient patient = (Patient)rows[0];

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {                           
                    (window as MainWindow).MainContentControl.DataContext = new AddToOrderViewModel(patient);
                    return;
                }
            }          
        }
    }
}
