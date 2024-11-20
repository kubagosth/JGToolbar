using System.Drawing;
using System.Windows.Forms;

namespace JGToolbar.TrayIcon
{
    public class SystemTrayIconManager
    {
        private NotifyIcon notifyIcon;

        public SystemTrayIconManager()
        {
            notifyIcon = new NotifyIcon
            {
                Icon = GetTemporaryIcon(),
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

            AppDomain.CurrentDomain.ProcessExit += (s, e) => notifyIcon.Dispose();
        }

        private Icon GetTemporaryIcon()
        {
            return SystemIcons.Application; //TODO: Replace the icon
        }

        private void OnSettingsClicked(object sender, EventArgs e)
        {
            SettingsWindow settingsWindow = new();
            settingsWindow.Show();
        }

        private void OnAboutClicked(object sender, EventArgs e)
        {
            MessageBox.Show("About... //TODO", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            notifyIcon.Dispose();
            Environment.Exit(0);
        }
    }
}
