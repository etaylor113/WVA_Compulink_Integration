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
            try
            {
                string ipNumText = File.ReadAllText(Paths.DSNFile);
                string apiKeyText = File.ReadAllText(Paths.apiKeyFile);

                if (ipNumText.Trim() != "" && apiKeyText.Trim() != "")
                {
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    Close();
                }
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
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
                AppError.PrintToLog(x);
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
                AppError.PrintToLog(x);
            }
        }

        private void WriteToFiles()
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
            if (!File.Exists(Paths.apiKeyFile))
            {
                Directory.CreateDirectory(Paths.apiKeyDir);
                var apiKeyFile = File.Create(Paths.apiKeyFile);
                apiKeyFile.Close();
            }

            File.WriteAllText(Paths.apiKeyFile, ApiKeyTextBox.Text.Trim());

            CheckFields();
        }

    }
}
