using System;
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
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Utility.File;

namespace WVA_Compulink_Integration.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            SetUp();
        }

        private void SetUp()
        {
            try
            {
                AccountTextBox.Focus();

                string actNumText = File.ReadAllText(Paths.ActNumFile);            
                AccountTextBox.Text = actNumText;
            }
            catch
            {

            }         
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(Paths.ActNumFile))
                {
                    File.WriteAllText(Paths.ActNumFile, AccountTextBox.Text);
                }
                else
                {
                    Directory.CreateDirectory(Paths.ActNumDir);
                    var actNumFile = File.Create(Paths.ActNumFile);
                    actNumFile.Close();
                    File.WriteAllText(Paths.ActNumFile, AccountTextBox.Text);
                }

                NotifyLabel.Visibility = Visibility.Visible;
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }        
        }

        private void AccountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NotifyLabel.Visibility = Visibility.Hidden;
        }
    }
}
