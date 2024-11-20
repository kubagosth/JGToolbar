using System.Configuration;
using System.Windows;
using JGToolbar.Helper;

namespace JGToolbar.Windows
{
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();

            AppNameText.Text = ConfigurationHelper.GetSettingValue<string>("AppName");
            VersionText.Text = $"Version {ConfigurationHelper.GetSettingValue<string>("Version")}";
            DeveloperText.Text = $"Developer: {ConfigurationHelper.GetSettingValue<string>("Developer")}";
            DescriptionText.Text = ConfigurationHelper.GetSettingValue<string>("Description");
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
