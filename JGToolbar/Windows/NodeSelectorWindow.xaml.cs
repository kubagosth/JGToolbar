using System.Windows;

namespace JGToolbar.Windows
{
    public partial class NodeSelectorWindow : Window
    {
        public NodeSelectorWindow()
        {
            InitializeComponent();
            LoadInstalledVersions();
        }

        private void LoadInstalledVersions()
        {
            throw new NotImplementedException();
        }


        private void InstallButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
