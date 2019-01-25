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
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Models;
using WVA_Compulink_Integration.Models.User;
using WVA_Compulink_Integration.Models.Validations.Emails;

namespace WVA_Compulink_Integration.Views.Login
{
    /// <summary>
    /// Interaction logic for ForgotPasswordWindow.xaml
    /// </summary>
    public partial class ForgotPasswordWindow : Window
    {
        public ForgotPasswordWindow()
        {
            InitializeComponent();
        }

        // Brings window to front without overlapping any following windows opened by user
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Topmost = true;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Topmost = false;
            MessageLabel.Visibility = Visibility.Hidden;
        } 

        private void ShowError()
        {
            MessageLabel.Visibility = Visibility.Visible;
            MessageLabel.Content = "Error sending email!";
        }

        private void SendEmailButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = "";

                // Get the email from username            
                 string getEmailEndpoint = "http://localhost:56075/CompuClient/User/GetEmail";
                User user = new User()
                {
                    UserName = UserNameTextBox.Text
                };

                string strEmail = API.Post(getEmailEndpoint, user);
                User userResponse = JsonConvert.DeserializeObject<User>(strEmail);

                if (userResponse.Status == "ERROR")
                {
                    ShowError();
                }
                else if (userResponse.Email != null)
                {
                    email = userResponse.Email;
                }
                else
                {
                    ShowError();
                }

                // Send the email
                string endpoint = "https://orders-qa.wisvis.com/mailers/reset";
                EmailValidationSend emailValidation = new EmailValidationSend()
                {
                    Email = email,
                    ApiKey = "426761f0-3e9d-4dfd-bdbf-0f35a232c285"
                };

                string strResponse = API.Post(endpoint, emailValidation);
                Response response = JsonConvert.DeserializeObject<Response>(strResponse);

                if (response.Status == "FAIL")
                {
                    ShowError();
                }
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            } 
           
        }

        private void SubmitCodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string endpoint = "https://orders-qa.wisvis.com/mailers/reset_check";
                EmailValidationCode emailValidation = new EmailValidationCode()
                {
                    EmailCode = CodeTextBox.Text.Trim(),
                    ApiKey = "426761f0-3e9d-4dfd-bdbf-0f35a232c285"
                };

                string strResponse = API.Post(endpoint, emailValidation);
                Response response = JsonConvert.DeserializeObject<Response>(strResponse);

                if (response.Status == "SUCCESS")
                {
                    new ChangePasswordWindow().Show();
                    Close();
                }
                else
                {
                    MessageLabel.Visibility = Visibility.Visible;
                    MessageLabel.Content = "Invalid Code!";
                }
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }       
        }

        private void CodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessageLabel.Visibility = Visibility.Hidden;
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessageLabel.Visibility = Visibility.Hidden;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }   
    }
}
