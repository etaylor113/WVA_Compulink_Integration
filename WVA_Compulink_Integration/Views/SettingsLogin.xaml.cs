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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WVA_Compulink_Integration.ViewModels;

namespace WVA_Compulink_Integration.Views
{
    /// <summary>
    /// Interaction logic for SettingsLogin.xaml
    /// </summary>
    public partial class SettingsLogin : UserControl
    {
        public SettingsLogin()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetUpView();
        }

        private void SetUpView()
        {
            PasswordTextBox.Focus();
            NotifyLabel.Visibility = Visibility.Hidden;
        }

        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            NotifyLabel.Visibility = Visibility.Hidden;
            if (e.Key == Key.Enter)
            {
                if (PasswordTextBox.Password == GetPassword())
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            (window as MainWindow).MainContentControl.DataContext = new SettingsViewModel();
                            return;
                        }
                    }
                }
                else
                {
                    NotifyLabel.Visibility = Visibility.Visible;
                    PasswordTextBox.Clear();
                }
            }
        }

        private string GetPassword()
        {
            string password = "evan";
            return password;
        }       

    }
}
