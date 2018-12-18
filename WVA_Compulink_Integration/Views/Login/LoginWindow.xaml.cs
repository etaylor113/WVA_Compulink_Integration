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
            SetUpUser();

            UsernameTextBox.Focus();
        }

        public void SetUpUser()
        {
            User.UserName = "evant123";
            User.ApiKey = "123456789";
        }

        public string VerifyUser()
        {
            try
            {
                string endpoint = "http://localhost:56075/CompuClient/User/Verify/" + UsernameTextBox.Text + "?key=" + User.ApiKey + "&pass=" + PasswordTextBox.Password.ToString();
                return API.Get(endpoint, out string httpStatus);
            }
            catch (Exception e)
            {
                return $"ERROR: {e.Message}";
            }
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
                var tempUser = VerifyUser();

                if (tempUser.ToString().Contains("ERROR"))
                {
                    ErrorWindow errorWindow = new ErrorWindow();
                    errorWindow.Show();
                    errorWindow.MessagesTextBox.Document.Blocks.Add(new Paragraph(new Run($"{tempUser}")));

                    return;
                }

                var verifiedUser = JsonConvert.DeserializeObject<VerifyUser>(tempUser);

                // Smart Verify. Will clear and place cursor on the incorrect field and give user a status. 
                if (verifiedUser.UserName != "OK")
                {
                    UsernameTextBox.Focus();
                    UsernameTextBox.Clear();
                    PasswordTextBox.Clear();
                    NotifyLabel.Content = "Invalid Username/Password.";
                }
                else if (verifiedUser.PassWord != "OK")
                {
                    UsernameTextBox.Focus();
                    UsernameTextBox.Clear();
                    PasswordTextBox.Clear();
                    NotifyLabel.Content = "Invalid Username/Password.";
                }
                else if (verifiedUser.ApiKey != "OK")
                {
                    NotifyLabel.Content = "Invalid Key. Contact IT.";
                }
                else if (verifiedUser.UserName == "ERROR")
                {
                    NotifyLabel.Content = "An error has occurred.";
                }
                else
                {
                    new MainWindow().Show();
                    Close();
                }
            }
            catch (Exception x)
            {
                new ReportError(x);
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
                new ReportError(x);
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
                new ReportError(x);
            }
        }

        private void BackToLogin()
        {
            new RegistrationWindow().Show();
            Close();
        }
        
    }
}
