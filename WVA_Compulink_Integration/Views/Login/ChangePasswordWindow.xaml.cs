using Newtonsoft.Json;
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
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Models;
using WVA_Compulink_Integration.Models.User;
using WVA_Compulink_Integration.Utility.File;

namespace WVA_Compulink_Integration.Views.Login
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        private string DSN { get; set; }
        private string UserName { get; set; }

        public ChangePasswordWindow(string userName)
        {
            DSN = File.ReadAllText(Paths.DSNFile).Trim();
            UserName = userName;
            InitializeComponent();          
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SubmitCodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Password == PasswordConfTextBox.Password)
            {
                Response response = ChangePassword();

                if (response.Status == "SUCCESS")
                {
                    Close();
                }
                else
                {
                    MessageLabel.Visibility = Visibility.Visible;
                    MessageLabel.Content = "An error has occurred. Password not updated";
                }
            }
            else
            {
                MessageLabel.Visibility = Visibility.Visible;
                MessageLabel.Content = "Passwords must match!";
            }
        }

        private Response ChangePassword()
        {
            string endpoint = $"http://{DSN}/api/user/changePass";

            User changePassUser = new User()
            {
                Password = Cryptography.Crypto.ConvertToHash(PasswordTextBox.Password),
                UserName = UserName
            };

            string strResponse = API.Post(endpoint, changePassUser);
            return JsonConvert.DeserializeObject<Response>(strResponse);           
        }

    }
}
