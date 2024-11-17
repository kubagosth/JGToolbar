using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGToolbar
{
    public class StartupManager
    {
        private const string AppName = "JGToolbar";

        /// <summary>
        /// Set this application to run on startup.
        /// </summary>
        public static void SetStartup()
        {
            string exePath = Process.GetCurrentProcess().MainModule.FileName;
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (registryKey.GetValue(AppName) == null)
            {
                registryKey.SetValue(AppName, exePath);
            }
        }
    }
}
