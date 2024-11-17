using Microsoft.Win32; 
using SHDocVw;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Threading;

namespace JGToolbar
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer positionTimer;
        private double smoothingFactor = 0.5; 
        private const string AppName = "JGToolbar";

        public MainWindow()
        {
            InitializeComponent();
            StartupManager.SetStartup();
            PositionNearExplorer();
            StartPositionTracking();
        }

        /// <summary>
        /// Open a command prompt window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenCommandPrompt_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("cmd.exe");
        }

        /// <summary>
        /// Open a command prompt window in the current directory of the File Explorer window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Git_Click(object sender, RoutedEventArgs e)
        {
            ShellWindows shellWindows = new ShellWindows();

            // Loop through all the open Shell Windows (Explorer)
            foreach (InternetExplorer window in shellWindows)
            {
                // Check if the window is a File Explorer window (not a browser)
                if (Path.GetFileNameWithoutExtension(window.FullName).Equals("explorer", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = $"/c dotnet new gitignore",
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

        /// <summary>
        /// Position the toolbar near the File Explorer window.
        /// </summary>
        private void PositionNearExplorer()
        {
            IntPtr explorerHandle = FindWindow("CabinetWClass", null);
            if (explorerHandle != IntPtr.Zero)
            {
                if (IsWindowMinimized(explorerHandle))
                {
                    this.Hide();
                    return;
                }
                else
                {
                    this.Show();
                }

                RECT rect;
                if (GetWindowRect(explorerHandle, out rect))
                {
                    // Calculate target positions for bottom-center alignment
                    double targetLeft = rect.Left + (rect.Right - rect.Left) / 2 - (this.Width / 2); // Center horizontally
                    double targetTop = rect.Bottom - this.Height - 10; // Position slightly above bottom edge (adjust margin)

                    // Smooth movement using linear interpolation
                    this.Left += (targetLeft - this.Left) * smoothingFactor;
                    this.Top += (targetTop - this.Top) * smoothingFactor;

                    // Snap to target position if close enough
                    if (Math.Abs(targetLeft - this.Left) < 1 && Math.Abs(targetTop - this.Top) < 1)
                    {
                        this.Left = targetLeft;
                        this.Top = targetTop;
                    }
                }
            }
            else
            {
                this.Hide();
            }
        }

        /// <summary>
        /// Start tracking the position of the toolbar near the File Explorer window.
        /// </summary>
        private void StartPositionTracking()
        {
            positionTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1)
            };
            positionTimer.Tick += (sender, args) => PositionNearExplorer();
            positionTimer.Start();
        }

        /// <summary>
        /// Check if a window is minimized.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        private bool IsWindowMinimized(IntPtr hWnd)
        {
            return IsIconic(hWnd);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}