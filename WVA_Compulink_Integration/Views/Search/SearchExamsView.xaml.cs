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
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Models.Exam;
using WVA_Compulink_Integration.Models.Patient;
using WVA_Compulink_Integration.ViewModels;
using WVA_Compulink_Integration.ViewModels.Search;
using WVA_Compulink_Integration.Views.Error;

namespace WVA_Compulink_Integration.Views.Search
{
    /// <summary>
    /// Interaction logic for SearchExamsView.xaml
    /// </summary>

    public partial class SearchExamsView : UserControl
    {

        DateTime date = DateTime.Now;
        
        

        public SearchExamsView()
        {
            
            InitializeComponent();
            RunExamViewSetup();
        }

        private void RunExamViewSetup()
        {
            SetUpExamDataGrid(date.ToString("yyyy-MM-dd"));         
        }


        private void ResetUI()
        {
            ExamDataGrid.Items.Clear();
        }

        private async void SetUpExamDataGrid(string date)
        {
            // Spawn a loading window and change cursor to waiting cursor
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {             
                //Change DateLabel
                DateLabel.Content = date;

                // Reset DataGrid
                ResetUI();

                // Get exam data
                List<Exam> listExams = await GetExamsData(date);

                if (listExams[0].PatientID != "0" && listExams[0].FirstName != null)
                {
                    // Input DataGrid data
                    LoadDataGrid(listExams);
                }

                // Notify user if there are no exams for the given day
                if (ExamDataGrid.Items.Count > 0)
                    NoExamsLabel.Visibility = Visibility.Hidden;
                else
                    NoExamsLabel.Visibility = Visibility.Visible;

                // Close loading window and change cursor back to default arrow cursor
                loadingWindow.Close();
                Mouse.OverrideCursor = Cursors.Arrow;
            }
            catch(Exception x)
            {
                AppError.PrintToLog(x);
                ErrorWindow errorWindow = new ErrorWindow("An error was encountered while attempting to find exams. \nSee error log.");
                errorWindow.Show();             
            }
            finally
            {
                loadingWindow.Close();
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        private async Task<List<Exam>> GetExamsData(string date)
        {
            try
            {
                string strExams = await Task.Run(() => SearchExamsViewModel.GetExamsAsync(date));
                return JsonConvert.DeserializeObject<List<Exam>>(strExams);
            }
            catch(Exception x)
            {              
                return null;
            }
        } 

        private void LoadDataGrid(List<Exam> listExams)
        {
            foreach (Exam exam in listExams)
            {
                ExamDataGrid.Items.Add(exam);
            }
        }

        private void RefreshDataBtn_Click(object sender, RoutedEventArgs e)
        {
            ResetUI();
            SetUpExamDataGrid(date.ToString("yyyy-MM-dd"));
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IList rows = ExamDataGrid.SelectedItems;
            Exam exam = (Exam)rows[0];

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).MainContentControl.DataContext = new AddToOrderViewModel(exam);
                    return;
                }
            }
        }

        private void PrevDayBtn_Click(object sender, RoutedEventArgs e)
        {
            // Set the DateTime object to previous day
            date = date.AddDays(-1);
            string prevDay = date.ToString("yyyy-MM-dd");
            SetUpExamDataGrid(prevDay);
        }

        private void NextDayBtn_Click(object sender, RoutedEventArgs e)
        {
            // Set the DateTime object to next day
            date = date.AddDays(1);
            string nextDay = date.ToString("yyyy-MM-dd");
            SetUpExamDataGrid(nextDay);
        }
    }
}
