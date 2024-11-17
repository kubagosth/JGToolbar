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
        /// Open Visual Studio Code.
        /// </summary>
        public void OpenVisualStudioCode()
        {
            string path = GetCurrentExplorerPath();
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/C code \"{path}\"",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to open Visual Studio Code: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("No active File Explorer window found.");
            }
        }





        /// <summary>
        /// Open a command prompt window in the current directory of the File Explorer window.
        /// </summary>
        public void OpenGitCommand()
        {
            string currentExplorerPath = GetCurrentExplorerPath();
            if (!string.IsNullOrEmpty(currentExplorerPath))
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = "/c dotnet new gitignore",
                        WorkingDirectory = currentExplorerPath,
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };
                    Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to open Git command: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("No active File Explorer window found.");
            }
        }

        /// <summary>
        /// Get the current path of the File Explorer window.
        /// </summary>
        /// <returns></returns>
        private string GetCurrentExplorerPath()
        {
            ShellWindows shellWindows = new ShellWindows();
            foreach (InternetExplorer window in shellWindows)
            {
                if (Path.GetFileNameWithoutExtension(window.FullName).Equals("explorer", StringComparison.OrdinalIgnoreCase))
                {
                    return new Uri(window.LocationURL).LocalPath;
                }
            }
            return null;
        }
    }
}
