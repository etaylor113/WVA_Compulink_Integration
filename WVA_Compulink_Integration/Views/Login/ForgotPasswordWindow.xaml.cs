﻿using Newtonsoft.Json;
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
using WVA_Compulink_Integration.Models.User;
using WVA_Compulink_Integration.Models.Validations.Emails;
using WVA_Compulink_Integration.Utility.File;

namespace WVA_Compulink_Integration.Views.Login
{
    /// <summary>
    /// Interaction logic for ForgotPasswordWindow.xaml
    /// </summary>
    public partial class ForgotPasswordWindow : Window
    {
        private string API_Key { get; set; }
        private string DSN { get; set; }

        public ForgotPasswordWindow()
        {
            InitializeComponent();

            try
            {
                API_Key = File.ReadAllText(Paths.apiKeyFile).Trim() ?? throw new NullReferenceException();
                DSN = File.ReadAllText(Paths.DSNFile).Trim() ?? throw new NullReferenceException();
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }           
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
                if (UserNameTextBox.Text.Trim() == "")
                {
                    MessageLabel.Visibility = Visibility.Visible;
                    MessageLabel.Content = "Enter username!";
                    return;
                }

                string email = "";
                
                string getEmailEndpoint = $"http://{DSN}/api/User/GetEmail";
                User user = new User()
                {
                    UserName = UserNameTextBox.Text
                };

                string strEmail = API.Post(getEmailEndpoint, user);
                User userResponse = JsonConvert.DeserializeObject<User>(strEmail);

                if (userResponse?.Status == "ERROR")
                {
                    ShowError();
                }
                else if (userResponse?.Email != null)
                {
                    email = userResponse.Email;
                }
                else
                {
                    ShowError();
                }

                // Send the email               
                string endpoint = $"http://{DSN}/api/user/reset-email";

                EmailValidationSend emailValidation = new EmailValidationSend()
                {
                    Email = email,
                    ApiKey = API_Key
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
                if (UserNameTextBox.Text.Trim() == "")
                {
                    MessageLabel.Visibility = Visibility.Visible;
                    MessageLabel.Content = "Enter username!";
                    return;
                }


                string endpoint = $"http://{DSN}/api/user/reset-email-check";
                EmailValidationCode emailValidation = new EmailValidationCode()
                {
                    EmailCode = CodeTextBox.Text.Trim(),
                    ApiKey = API_Key
                };

                string strResponse = API.Post(endpoint, emailValidation);
                Response response = JsonConvert.DeserializeObject<Response>(strResponse);

                if (response.Status == "SUCCESS")
                {
                    new ChangePasswordWindow(UserNameTextBox.Text.Trim()).Show();
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
