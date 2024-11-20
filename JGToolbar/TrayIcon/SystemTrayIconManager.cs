using JGToolbar.Windows;
using System.Drawing;
using System.Windows.Forms;

namespace JGToolbar.TrayIcon
{
    public class SystemTrayIconManager
    {
        private NotifyIcon notifyIcon;
        private SettingsWindow settingsWindow;
        private AboutWindow aboutWindow;

        public SystemTrayIconManager()
        {
            notifyIcon = new NotifyIcon
            {
                Icon = new Icon("Assets/Icon.ico"),
                Visible = true,
                Text = "JG Toolbar"
            };

            // Create the ContextMenuStrip and apply the custom renderer
            var contextMenu = new ContextMenuStrip();
            contextMenu.Renderer = new CustomContextMenuRenderer(); 

            // Context menu items
            var settingsItem = new ToolStripMenuItem("Settings", null, OnSettingsClicked);
            var aboutItem = new ToolStripMenuItem("About", null, OnAboutClicked);
            var exitItem = new ToolStripMenuItem("Exit", null, OnExitClicked);

            contextMenu.Items.Add(settingsItem);
            contextMenu.Items.Add(aboutItem);
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add(exitItem);

            notifyIcon.ContextMenuStrip = contextMenu;

            AppDomain.CurrentDomain.ProcessExit += (sender, args) => notifyIcon.Dispose();
        }

        private void OnSettingsClicked(object sender, EventArgs e)
        {
            if (settingsWindow == null || !settingsWindow.IsVisible)
            {
                settingsWindow = new SettingsWindow();
                settingsWindow.Closed += (sender, args) => settingsWindow = null; 
                settingsWindow.Show();
            }
            else
            {
                settingsWindow.Activate(); 
            }
        }

        private void OnAboutClicked(object sender, EventArgs e)
        {
            if (aboutWindow == null || !aboutWindow.IsVisible)
            {
                aboutWindow = new AboutWindow();
                aboutWindow.Closed += (sender, args) => aboutWindow = null; 
                aboutWindow.Show();
            }
            else
            {
                aboutWindow.Activate(); 
            }
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            notifyIcon.Dispose();
            Environment.Exit(0);
        }
    }
}
