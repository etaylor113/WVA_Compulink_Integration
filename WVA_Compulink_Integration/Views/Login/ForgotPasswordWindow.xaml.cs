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
            AutoFillEmail();
                    
        }

        // Brings window to front without overlapping any following windows opened by user
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Topmost = true;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Topmost = false;
        }

        private void AutoFillEmail()
        {
            EmailTextBox.Text = GetEmail();
        }

        private string GetEmail()
        {
            // Make call to server to get email or get email during login process
            //
            //

            string email = "etaylor113@gmail.com";

            return email;
        }

        private void SendEmailButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;

            MessageLabel.Visibility = Visibility.Visible;
            // Send this email to the API and generate a 6 digit code

        }

        private void SubmitCodeButton_Click(object sender, RoutedEventArgs e)
        {
            string sixDigitCode = CodeTextBox.Text.Trim();

            // Make call to API and see if code they typed in is correct

            bool codeIsCorrect = false;

            if (sixDigitCode == "123456")
                codeIsCorrect = true;

            if (codeIsCorrect)
            {
                new ChangePasswordWindow().Show();
                Close();
            }                        
            else
                CodeTextBox.Text = "Invalid Code";
        }

        private void CodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessageLabel.Visibility = Visibility.Hidden;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
