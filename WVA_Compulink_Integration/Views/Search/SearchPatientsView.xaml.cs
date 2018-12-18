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
            //SetUpPatientDataGrid();
        }

        private void ResetUI()
        {
            SearchTextBox.Clear();
            PatientDataGrid.Items.Clear();           
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
            // Filter DataGrid every time a keystroke is detected in SearchTextBox
            if (QuickSearchCheckBox.IsChecked ?? false)
            {
                SearchPatients(PatientFilterComboBox.SelectedIndex, SearchTextBox.Text);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {      
            // Run search function without quick search. Better performance for high number of columns
            SearchPatients(PatientFilterComboBox.SelectedIndex, SearchTextBox.Text);
        }  

        private void SearchPatients(int index, string searchBoxText)
        {
            try
            {                  
                // Clear copied list of Patients 
                ListPatients.CopyListPatients.Clear();

                // Populate table with original list of patients that are loaded from api call
                foreach (Patient patient in ListPatients.OrigListPatients)
                {
                    PatientDataGrid.Items.Add(patient);
                    ListPatients.CopyListPatients.Add(patient);
                }

                for (int i = ListPatients.CopyListPatients.Count - 1; i >= 0; i--)
                {
                    // Based on the index selected in the combobox, iterate over a different part of the copied list of patients
                    switch (index)
                    {
                        // PatientID
                        case 0:                           
                            if (!ListPatients.CopyListPatients[i].PatientID.ToString().StartsWith(searchBoxText))                            
                                ListPatients.CopyListPatients.RemoveAt(i);                                                       
                            break;
                        // Name
                        case 1:
                            string CompleteName = $"{ListPatients.CopyListPatients[i].FirstName} {ListPatients.CopyListPatients[i].LastName}";
                            
                            if (!CompleteName.StartsWith(searchBoxText))
                                ListPatients.CopyListPatients.RemoveAt(i);                        
                            break;                 
                        // Street
                        case 2:                           
                            if (!ListPatients.CopyListPatients[i].Street.StartsWith(searchBoxText))
                                ListPatients.CopyListPatients.RemoveAt(i);                         
                            break;
                        // City
                        case 3:                           
                            if (!ListPatients.CopyListPatients[i].City.StartsWith(searchBoxText))
                                ListPatients.CopyListPatients.RemoveAt(i);                          
                            break;
                        // State
                        case 4:                            
                            if (!ListPatients.CopyListPatients[i].State.StartsWith(searchBoxText))
                                ListPatients.CopyListPatients.RemoveAt(i);                         
                            break;
                        // Zip
                        case 5:                            
                            if (!ListPatients.CopyListPatients[i].Zip.StartsWith(searchBoxText))
                                ListPatients.CopyListPatients.RemoveAt(i);                       
                            break;
                        // Phone
                        case 6:                           
                            if (!ListPatients.CopyListPatients[i].Phone.StartsWith(searchBoxText))
                                ListPatients.CopyListPatients.RemoveAt(i);                           
                            break;
                        default:
                            break;
                    }
                }

                // Clear table items
                PatientDataGrid.Items.Clear();

                // Populate table with updated list of patients based on search criteria
                foreach (Patient patient in ListPatients.CopyListPatients)
                {
                    PatientDataGrid.Items.Add(patient);
                }
            }
            catch (Exception x)
            {
                ReportError error = new ReportError(x);
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
