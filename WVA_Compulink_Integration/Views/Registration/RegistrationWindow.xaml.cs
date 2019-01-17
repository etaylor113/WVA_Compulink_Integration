using Newtonsoft.Json;
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
using System.Windows.Shapes;
using WVA_Compulink_Integration.Cryptography;
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Models.User;
using WVA_Compulink_Integration.Views.Login;

namespace WVA_Compulink_Integration.Views.Registration
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            try
            {
                InitializeComponent();
                SetUp();
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
        }

        private void SetUp()
        {
            EmailTextBox.Focus();
            ChangeToDefault();
        }

        private void ChangeToDefault()
        {
            NotifyLabel.Visibility = Visibility.Hidden;
            Height = 360;
        }

        private void MessageSetup(string notifyMessage)
        {
            NotifyLabel.Visibility = Visibility.Visible;
            NotifyLabel.Text = notifyMessage;
            Height = 420;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void BackToLogin()
        {
            new LoginWindow().Show();
            Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //
                // Pre API reponse check
                //
                if (!IsValidEmail(EmailTextBox.Text))
                {
                    MessageSetup("Email is not valid!");
                    return;
                }
                // Make sure password length isn't to short 
                if (PasswordTextBox.Password.ToString().Length < 6)
                {
                    MessageSetup("Password must be at least 6 characters!");
                    return;
                }
                // Check if password and confirm password match 
                else if (PasswordTextBox.Password.ToString() != ConfirmPasswordTextBox.Password.ToString())
                {
                    MessageSetup("Passwords must match!");
                    return;
                }
                // Check for blank fields 
                else if (EmailTextBox.Text.Trim() == "" || UserNameTextBox.Text.Trim() == "" || PasswordTextBox.Password.ToString().Trim() == "" || ConfirmPasswordTextBox.Password.ToString().Trim() == "")
                {
                    MessageSetup("Field cannot be blank!");
                    return;
                }

                //
                // Post API response check
                //

                User user = new User()
                {
                    UserName = UserNameTextBox.Text,
                    Password = Crypto.ConvertToHash(PasswordTextBox.Password),
                    Email = EmailTextBox.Text,
                    ApiKey = ""
                };

                string registerResponse = _API.API.Post("http://localhost:56075/CompuClient/User/register", user, out string httpStatus);        
                User userRegisterResponse = JsonConvert.DeserializeObject<User>(registerResponse);

                // Check if email exists
                if (userRegisterResponse.Message == "Email already exists")
                {
                    MessageSetup("Email is already in use!");
                }
                if (userRegisterResponse.Message == "UserName already exists")
                {
                    MessageSetup("Username is already in use!");
                }
                else if (userRegisterResponse.Status == "ERROR")
                {
                    throw new Exception($"Server responded with the following error: {userRegisterResponse.Message}");
                }
                else if (userRegisterResponse.Status == "OK")
                {
                    new MainWindow().Show();
                    Close();
                }
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
                MessageSetup("An error has occurred. If the problem persists, please contact IT.");
            }
        }

        private void BackToLoginLink_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BackToLogin();   
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
        }
       
        private void ActNumTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeToDefault();
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeToDefault();
        }

        private void LocationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeToDefault();
        }

        private void UserNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeToDefault();
        }

        private void PasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ChangeToDefault();
        }

        private void ConfirmPasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ChangeToDefault();
        }
    }
}
