using Microsoft.Win32;
using System.Windows;

namespace JGToolbar
{
    public partial class SettingsWindow : Window
    {
        private const string AppName = "JGToolbar";
        private const string RunRegistryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        public SettingsWindow()
        {
            InitializeComponent();
            LoadAutorunSetting();
        }

        private void LoadAutorunSetting()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RunRegistryKey, false))
            {
                if (key != null)
                {
                    string value = (string)key.GetValue(AppName);
                    AutorunCheckBox.IsChecked = !string.IsNullOrEmpty(value);
                }
            }
        }

        private void AutorunCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SetAutorun(true);
        }

        private void AutorunCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SetAutorun(false);
        }

        private void SetAutorun(bool enable)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RunRegistryKey, true))
            {
                if (key != null)
                {
                    if (enable)
                    {
                        string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        key.SetValue(AppName, $"\"{exePath}\"");
                    }
                    else
                    {
                        key.DeleteValue(AppName, false);
                    }
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
