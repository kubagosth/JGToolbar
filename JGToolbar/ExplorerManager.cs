using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using SHDocVw;

namespace JGToolbar
{
    public class ExplorerManager
    {
        /// <summary>
        /// Open a command prompt window.
        /// </summary>
        public void OpenCommandPrompt()
        {
            Process.Start("cmd.exe");
        }

        /// <summary>
        /// Open a command prompt window in the current directory of the File Explorer window.
        /// </summary>
        public void OpenGitCommand()
        {
            ShellWindows shellWindows = new ShellWindows();

            foreach (InternetExplorer window in shellWindows)
            {
                if (Path.GetFileNameWithoutExtension(window.FullName).Equals("explorer", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = "/c dotnet new gitignore",
                            WorkingDirectory = new Uri(window.LocationURL).LocalPath,
                            CreateNoWindow = true,
                            UseShellExecute = false
                        };
                        Process.Start(startInfo);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
