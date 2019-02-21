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
using WVA_Compulink_Integration.Cryptography;
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Models.User;
using WVA_Compulink_Integration.Utility.File;
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
                // Check if string is in an email-like format
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsComplexPassword(string password)
        {
            // Password must be at least 8 characters
            if (password.Length < 8)             
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

                string password = PasswordTextBox.Password.ToString();
                string confirmPassword = ConfirmPasswordTextBox.Password.ToString();
                string email = EmailTextBox.Text;
                string username = UserNameTextBox.Text;

                if (!IsValidEmail(email))
                {
                    MessageSetup("Email is not valid!");
                    return;
                }
                // Make sure password length isn't to short 
               
                // Check if password and confirm password match 
                if (password != confirmPassword)
                {
                    MessageSetup("Passwords must match!");
                    return;
                }
                // Check for blank fields 
                else if (email.Trim() == "" || username.Trim() == "" || password.Trim() == "" || confirmPassword.Trim() == "")
                {
                    MessageSetup("Field cannot be blank!");
                    return;
                }
                else if (!IsComplexPassword(password))
                {
                    MessageSetup("Password must be a minimum of 8 characters, have one capital letter, and contain at least one number.");
                    return;
                }

                //
                // Post API response check
                //

                User user = new User()
                {
                    UserName = username,
                    Password = Crypto.ConvertToHash(password),
                    Email = email,
                    ApiKey = ""
                };

                string dsn = File.ReadAllText(Paths.DSNFile).Trim();
                string endpoint = $"http://{dsn}/api/User/register";
                string registerResponse = _API.API.Post(endpoint, user);        
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
                    UserData._User = userRegisterResponse;
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
