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
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Models.Response;
using WVA_Compulink_Integration.Models.Users;
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

        private bool IsComplexPassword(string password)
        {
            try { 
                // Password must be at least 8 characters
                if (password == null || password.Length < 8)
                    return false;

                bool hasCapitalLetter = false;
                bool hasNumber = false;

                foreach (char letter in password)
                {
                    // Check password for capital letters
                    if (char.IsUpper(letter) && char.IsLetter(letter))
                        hasCapitalLetter = true;

                    // Check password for numbers
                    if (char.IsNumber(letter))
                        hasNumber = true;
                }

                if (hasCapitalLetter && hasNumber)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
                return false;
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

            if (strResponse == null || strResponse.ToString().Trim() == "")
                throw new NullReferenceException("response from endpoint was null or empty");
            else
                return JsonConvert.DeserializeObject<Response>(strResponse);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SubmitCodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Make sure passwords match
                if (PasswordTextBox.Password != PasswordConfTextBox.Password)
                {
                    MessageLabel.Visibility = Visibility.Visible;
                    MessageLabel.Text = "Passwords must match!";
                    return;
                }

                // Make sure password is complex
                if (!IsComplexPassword(PasswordTextBox.Password))
                {
                    MessageLabel.Visibility = Visibility.Visible;
                    MessageLabel.Text = "Password must be a minimum of 8 characters, have one capital letter, and contain at least one number.";
                    Height = 350;
                    return;
                }

                // Make request to change password
                Response response = ChangePassword();

                if (response?.Status == "SUCCESS")
                {                     
                    new MessageWindow("\t\tPassword updated!").Show();
                    Close();
                }
                else
                    throw new Exception($"Bad response from endpoint. Status: {response?.Status} -- Message: {response?.Message}.");
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
                MessageLabel.Visibility = Visibility.Visible;
                MessageLabel.Text = "An error has occurred. Password not updated. If this problem persists, please contact IT.";
                Height = 350;
            }
        }

    }
}
