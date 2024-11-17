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
        private readonly ExplorerManager explorerManager;
        private readonly WindowPositionManager windowPositionManager;

        /// <summary>
        /// Initialize the main window.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            explorerManager = new ExplorerManager();
            windowPositionManager = new WindowPositionManager(this);
            StartupManager.SetStartup();

            StartPositionTracking();
        }

        /// <summary>
        /// Open a command prompt window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenCommandPrompt_Click(object sender, RoutedEventArgs e)
        {
            explorerManager.OpenCommandPrompt();
        }

        /// <summary>
        /// Open Visual Studio Code.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenVisualStudioCode_Click(object sender, RoutedEventArgs e)
        {
            explorerManager.OpenVisualStudioCode();
        }

        /// <summary>
        /// Open a command prompt window in the current directory of the File Explorer window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Git_Click(object sender, RoutedEventArgs e)
        {
            explorerManager.OpenGitCommand();
        }

        /// <summary>
        /// Start tracking the position of the window.
        /// </summary>
        private void StartPositionTracking()
        {
            positionTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1)
            };
            positionTimer.Tick += (sender, args) => windowPositionManager.PositionNearExplorer();
            positionTimer.Start();
        }
    }
}