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
using WVA_Compulink_Integration.ViewModels.Search;

namespace WVA_Compulink_Integration.Views
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
            SearchContentControl.DataContext = new SearchPatientsViewModel();
        }

        private void SearchByPatientButton_Click(object sender, RoutedEventArgs e)
        {
            TabLabel.Content = "Search - Patients";

            Color blue = (Color)ColorConverter.ConvertFromString("#FF327EC3");
            SolidColorBrush blueBrush = new SolidColorBrush(blue);        
            PatientRect.Fill = blueBrush;

            Color white = (Color)ColorConverter.ConvertFromString("#ffffff");
            SolidColorBrush whiteBrush = new SolidColorBrush(white);        
            ExamsRect.Fill = whiteBrush;

            SearchContentControl.DataContext = new SearchPatientsViewModel();
        }

        private void SearchByExamsButton_Click(object sender, RoutedEventArgs e)
        {
            TabLabel.Content = "Search - Exams";

            Color blue = (Color)ColorConverter.ConvertFromString("#FF327EC3");
            SolidColorBrush blueBrush = new SolidColorBrush(blue);
            ExamsRect.Fill = blueBrush;

            Color white = (Color)ColorConverter.ConvertFromString("#ffffff");
            SolidColorBrush whiteBrush = new SolidColorBrush(white);
            PatientRect.Fill = whiteBrush;

           SearchContentControl.DataContext = new SearchExamsViewModel();
        }
    }
}
