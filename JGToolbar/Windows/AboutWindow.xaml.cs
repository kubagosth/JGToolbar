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

            string settingsPath = "Settings/AppSettings.json"; 
            var settings = ConfigurationHelper.LoadSettings(settingsPath);

            AppNameText.Text = settings.AppName;
            VersionText.Text = $"Version {settings.Version}";
            DeveloperText.Text = $"Developed by {settings.Developer}";
            DescriptionText.Text = settings.Description;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
