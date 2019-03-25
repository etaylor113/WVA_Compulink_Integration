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
using WVA_Compulink_Integration.Models.Users;
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Error;
using Newtonsoft.Json;
using WVA_Compulink_Integration.Views.Error;
using WVA_Compulink_Integration.Views.Registration;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Cryptography;
using System.IO;
using WVA_Compulink_Integration.Utility.File;

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
                    Password = Crypto.ConvertToHash(PasswordTextBox.Password),
                };

                string dsn = File.ReadAllText(Paths.DSNFile).Trim();
                string endpoint = $"http://{dsn}/api/User/login";
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

        private UserSettings GetUserSettings()
        {
            try
            {
                string defaultSettings = JsonConvert.SerializeObject(new UserSettings());

                if (!Directory.Exists(Paths.UserSettingsDir))
                    Directory.CreateDirectory(Paths.UserSettingsDir);

                if (!File.Exists(Paths.UserSettingsFile))
                {
                    File.Create(Paths.UserSettingsFile).Close();
                    File.WriteAllText(Paths.UserSettingsFile, defaultSettings);
                }

                return JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText($@"{Paths.UserSettingsFile}"));
            }
            catch (Exception ex)
            {
                AppError.PrintToLog(ex);
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
    

        private void Login()
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
                    UserData.Data = loginUserResponse;
                    UserData.Data.Settings = GetUserSettings();

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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login();
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

        private void UsernameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    Login();
                }
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
        }

        private void PasswordTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    Login();
                }
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
        }
    }
}
