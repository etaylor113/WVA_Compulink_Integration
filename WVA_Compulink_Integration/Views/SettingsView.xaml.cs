using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Models.Users;
using WVA_Compulink_Integration.Utility.Files;

namespace WVA_Compulink_Integration.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            SetUpUI();
            SetUpWvaAccountNumber();
        }

        private void SetUpUI()
        {
            try
            {
                DeleteBlankCompulinkOrdersCheckBox.IsChecked = UserData.Data?.Settings?.DeleteBlankCompulinkOrders;

                // Subscribe to AccountTextBox event delegate
                IsVisibleChanged += new DependencyPropertyChangedEventHandler(AvailableActsComboBox_IsVisibleChanged);
            }
            catch
            {
                DeleteBlankCompulinkOrdersCheckBox.IsChecked = false;
            }
        }

        private void SetUpWvaAccountNumber()
        {
            try
            {
                // Populate AvailableActComboBox with user's accounts
                List<string> availableActs = GetAvailableAccounts();

                // Check for nulls 
                if (availableActs == null || availableActs.ToString().Trim() == "" || availableActs?.Count < 1)
                    throw new NullReferenceException("No available account numbers detected.");

                // Add accounts to combo box
                foreach (string account in availableActs)
                    AvailableActsComboBox.Items.Add(account);

                // Pull account number from file if its there
                string actNum = File.ReadAllText(Paths.ActNumFile).Trim();

                // Select their account number if it's been set already in the drop down
                for (int i = 0; i < availableActs.Count; i++)
                {
                    if (availableActs[i] == actNum)
                        AvailableActsComboBox.SelectedIndex = i;
                }
            }
            catch (FileNotFoundException)
            {
                if (!Directory.Exists(Paths.ActNumDir))
                    Directory.CreateDirectory(Paths.ActNumDir);

                if (!File.Exists(Paths.ActNumFile))
                    File.Create(Paths.ActNumFile);

                SetUpWvaAccountNumber();
            }
            catch (Exception ex)
            {
                AppError.ReportOrLog(ex);
            }
        }

        private List<string> GetAvailableAccounts()
        {
            try
            {
                string endpoint = $"http://{UserData.Data?.DSN}/api/user/get-acts";
                string response = API.Get(endpoint, out string httpStatus);
                return JsonConvert.DeserializeObject<List<string>>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void UpdateUserSettings(bool deleteBlankCompulinkOrders)
        {
            try
            {
                // Updates user settings in memory
                UserData.Data.Settings.DeleteBlankCompulinkOrders = deleteBlankCompulinkOrders;

                // Updates user settings in settings file
                string userSettings = JsonConvert.SerializeObject(UserData.Data?.Settings);

                if (!Directory.Exists(Paths.UserSettingsDir))
                    Directory.CreateDirectory(Paths.UserSettingsDir);

                if (!File.Exists(Paths.UserSettingsFile))
                    File.Create(Paths.UserSettingsFile).Close();

                File.WriteAllText(Paths.UserSettingsFile, userSettings);
            }
            catch (Exception ex)
            {
                AppError.ReportOrLog(ex);
            }
        }

        // Allow SearchTextBox to get focus
        void AvailableActsComboBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if ((bool)e.NewValue == true)
                {
                    Dispatcher.BeginInvoke(
                    DispatcherPriority.ContextIdle,
                    new Action(delegate ()
                    {
                        AvailableActsComboBox.Focus();
                    }));
                }
            }
            catch (Exception ex)
            {
                AppError.ReportOrLog(ex);
            }
        }

        private void AvailableActsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (!File.Exists(Paths.ActNumFile))
                {
                    Directory.CreateDirectory(Paths.ActNumDir);
                    var actNumFile = File.Create(Paths.ActNumFile);
                    actNumFile.Close();
                }

                File.WriteAllText(Paths.ActNumFile, (sender as ComboBox).SelectedItem as string);              
            }
            catch (Exception x)
            {
                NotifyLabel.Visibility = Visibility.Visible;
                AppError.ReportOrLog(x);
            }
        }

        private void DeleteBlankCompulinkOrdersCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateUserSettings(deleteBlankCompulinkOrders: true);
        }

        private void DeleteBlankCompulinkOrdersCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateUserSettings(deleteBlankCompulinkOrders: false);
        }
    }
}
