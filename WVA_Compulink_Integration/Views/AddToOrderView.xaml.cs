using Newtonsoft.Json;
using System;
using System.Collections;
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
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Models.Patient;
using WVA_Compulink_Integration.Models.Prescription;
using WVA_Compulink_Integration.ViewModels;
using WVA_Compulink_Integration.ViewModels.Orders;
using WVA_Compulink_Integration.Views;

namespace WVA_Compulink_Integration.Views
{
    /// <summary>
    /// Interaction logic for AddToOrderView.xaml
    /// </summary>
    public partial class AddToOrderView : UserControl
    {
        public AddToOrderView()
        {
            InitializeComponent();
            SetUpUI();
        }    

        private void SetUpUI()
        {
            SetPatientNameLabel();
            SetUpPrescriptionDataGrid();
        }

        private void SetPatientNameLabel()
        {
            PatienNameLabel.Content = $"Prescription for: {AddToOrderViewModel.FirstName} {AddToOrderViewModel.LastName}";        
        }

        private List<Prescription> GetPatientPrescriptions(string patientID)
        {
            try
            {
                string endpoint = "http://localhost:56075/CompuClient/Prescriptions/" + patientID;
                string strPrescriptions = API.Get(endpoint, out string httpStatus);

                return JsonConvert.DeserializeObject<List<Prescription>>(strPrescriptions);
            }
            catch (Exception x)
            {
                return null;
            }         
        }

        private void SetUpPrescriptionDataGrid()
        {
            List<Prescription> listPrescriptions = new List<Prescription>();
            var prescriptions = GetPatientPrescriptions(AddToOrderViewModel.ID);
            
            foreach (Prescription prescription in prescriptions)
            {
                listPrescriptions.Add(prescription);
            }

            PrescriptionDataGrid.ItemsSource = listPrescriptions;
        }

        private void AddToSTOButton_Click(object sender, RoutedEventArgs e)
        {
            List<Prescription> prescriptions = new List<Prescription>();
            IList rows = PrescriptionDataGrid.Items;
            
            for (int i=0; i<rows.Count; i++)
            {
                Prescription prescription = (Prescription)rows[i];
                 if (prescription.IsChecked)
                 {
                    prescription.Patient = prescription.Patient;
                    prescriptions.Add(prescription);
                 }                       
            }

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).MainContentControl.DataContext = new OrdersViewModel("LabOrders", prescriptions);
                }
            }                
        }

        private void AddToSTPButton_Click(object sender, RoutedEventArgs e)
        {
            List<Prescription> prescriptions = new List<Prescription>();
            IList rows = PrescriptionDataGrid.Items;

            for (int i = 0; i < rows.Count; i++)
            {
                Prescription prescription = (Prescription)rows[i];
                if (prescription.IsChecked)
                {
                    prescription.Patient = prescription.Patient;
                    prescriptions.Add(prescription);
                }
            }

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).MainContentControl.DataContext = new OrdersViewModel("WVA_Orders", prescriptions);
                }
            }
        }

        private void PrescriptionDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Prescription prescription = (Prescription)PrescriptionDataGrid.SelectedItem;
       
            //PrescriptionDataGrid.Items.Refresh();
        }

        private void AddToOrderButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
