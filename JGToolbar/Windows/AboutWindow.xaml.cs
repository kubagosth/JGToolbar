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

            AppNameText.Text = ConfigurationHelper.GetSettingValue<string>("About.AppName");
            VersionText.Text = $"Version {ConfigurationHelper.GetSettingValue<string>("About.Version")}";
            DeveloperText.Text = $"Developer: {ConfigurationHelper.GetSettingValue<string>("About.Developer")}";
            DescriptionText.Text = ConfigurationHelper.GetSettingValue<string>("About.Description");
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
