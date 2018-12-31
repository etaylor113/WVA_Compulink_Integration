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
using WVA_Compulink_Integration.Error;
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
                ActNumTextBox.Focus();
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check if Account Number exists
                if (false)
                {

                }
                // Check if email exists
                else if (false)
                {

                }
                // Check if password and confirm password match 
                else if (PasswordTextBox.Password.ToString() != ConfirmPasswordTextBox.Password.ToString())
                {
                    NotifyLabel.Visibility = Visibility.Visible;
                    NotifyLabel.Content = "Passwords must match!";
                }
                // Check for blank fields 
                else if (ActNumTextBox.Text.Trim() == "" || FirstNameTextBox.Text.Trim() == "" || EmailTextBox.Text.Trim() == "" || PasswordTextBox.Password.ToString().Trim() == "" || ConfirmPasswordTextBox.Password.ToString().Trim() == "")
                {
                    NotifyLabel.Visibility = Visibility.Visible;
                    NotifyLabel.Content = "Field cannot be blank!";
                }
                else
                {
                    new MainWindow().Show();
                    Close();
                }
            }
            catch (Exception x)
            {
                AppError.PrintToLog(x);
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

        private void BackToLogin()
        {
            new LoginWindow().Show();
            Close();
        }

    }
}
