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
using WVA_Compulink_Integration.Models.User;
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Error;
using Newtonsoft.Json;
using WVA_Compulink_Integration.Views.Error;
using WVA_Compulink_Integration.Views.Registration;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Cryptography;

namespace WVA_Compulink_Integration.Views.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            SetUp();           
        }

        private void SetUp()
        {
            UsernameTextBox.Focus();
        }
  
        private User LoginUser()
        {
            try
            {
                User user = new User()
                {
                    UserName = UsernameTextBox.Text,
                    Password = Crypto.ConvertToHash(PasswordTextBox.Password)           
                };

                string endpoint = "http://localhost:56075/CompuClient/User/login";
                string loginResponse = API.Post(endpoint, user);
                User userLoginResponse = JsonConvert.DeserializeObject<User>(loginResponse);
                           
                return userLoginResponse;
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
                return null;
            }
        }

        private void ResetTextBoxes()
        {
            UsernameTextBox.Focus();
            UsernameTextBox.Clear();
            PasswordTextBox.Clear();
        }

        // ===========================================================================================================================
        // Form events called from LoginWindow 
        // ===========================================================================================================================
    
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Make NotifyLabel visible if necessary.
                NotifyLabel.Visibility = Visibility.Visible;

                // Verify user's credentials through the api and return verifiedUser object. 
                User loginUserResponse = LoginUser();             

                // Check login credentials                 
                if (loginUserResponse.Status == "ERROR" || loginUserResponse.Status == "FAIL")
                {
                    NotifyLabel.Visibility = Visibility.Visible;
                    NotifyLabel.Text = $"{loginUserResponse.Message}";
                    return;
                }               
                else if (loginUserResponse.Status == "OK")
                {
                    // Set user data in memory to response items
                    UserData._User = loginUserResponse;

                    // Let user continue into application
                    new MainWindow().Show();
                    Close();
                }
                else
                {
                    throw new Exception("Server was unable to provide a sufficient response.");
                }
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
                NotifyLabel.Visibility = Visibility.Visible;
                NotifyLabel.Text = "An error has occurred. If the problem persists, please contact IT.";
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ForgotPasswordLink_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ForgotPasswordWindow forgotPasswordWindow = new ForgotPasswordWindow();

                if (forgotPasswordWindow.IsActive)
                {
                    forgotPasswordWindow.Topmost = true;
                }
                else
                {
                    forgotPasswordWindow.Show();
                }
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
        }

        private void CreateAccountLink_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void BackToLogin()
        {
            new RegistrationWindow().Show();
            Close();
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NotifyLabel.Visibility = Visibility.Hidden;
        }

        private void PasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            NotifyLabel.Visibility = Visibility.Hidden;
        }
    }
}
