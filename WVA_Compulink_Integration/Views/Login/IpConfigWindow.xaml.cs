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
using System.Windows.Shapes;
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Utility.File;

namespace WVA_Compulink_Integration.Views.Login
{
    /// <summary>
    /// Interaction logic for IpConfigWindow.xaml
    /// </summary>
    public partial class IpConfigWindow : Window
    {
        public IpConfigWindow()
        {
            InitializeComponent();            
            IpConfigTextBox.Focus();
            CheckFields();
        }

        private void CheckFields()
        {
            string ipNumText = "";
            string apiKeyText = "";

            try
            {              
                try // Try to read the DSN
                {
                    ipNumText = File.ReadAllText(Paths.DSNFile);
                }
                catch
                {
                    Directory.CreateDirectory(Paths.DSNDir);
                    var dsnFile = File.Create(Paths.DSNFile);
                    dsnFile.Close();
                    ipNumText = File.ReadAllText(Paths.DSNFile);
                }

                try // Try to read the Api Key
                {
                    apiKeyText = File.ReadAllText(Paths.ApiKeyFile);
                }
                catch
                {
                    Directory.CreateDirectory(Paths.ApiKeyDir);
                    var apiKeyFile = File.Create(Paths.ApiKeyFile);
                    apiKeyFile.Close();
                    apiKeyText = File.ReadAllText(Paths.ApiKeyFile);
                }
                   
                // Open login window if DSN and Api key has been set
                if (ipNumText.Trim() != "" && apiKeyText.Trim() != "")
                {
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    Close();
                }
            }
            catch (Exception x)
            {
                AppError.ReportOrWrite(x);
            }           
        }

        private void WriteToFiles()
        {
            try
            {
                // Write to ipConfig file
                if (!File.Exists(Paths.DSNFile))
                {
                    Directory.CreateDirectory(Paths.DSNDir);
                    var ipNumFile = File.Create(Paths.DSNFile);
                    ipNumFile.Close();
                }

                File.WriteAllText(Paths.DSNFile, IpConfigTextBox.Text.Trim());

                // Write to apiKey file
                if (!File.Exists(Paths.ApiKeyFile))
                {
                    Directory.CreateDirectory(Paths.ApiKeyDir);
                    var apiKeyFile = File.Create(Paths.ApiKeyFile);
                    apiKeyFile.Close();
                }

                File.WriteAllText(Paths.ApiKeyFile, ApiKeyTextBox.Text.Trim());
                CheckFields();
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
            }
        }

        private void IpConfigTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    WriteToFiles();
                }               
            }
            catch (Exception x)
            {
                AppError.ReportOrWrite(x);
            }
        }

        private void ApiKeyTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    WriteToFiles();
                }
            }
            catch (Exception x)
            {
                AppError.ReportOrWrite(x);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            WriteToFiles();
        }
    }
}
