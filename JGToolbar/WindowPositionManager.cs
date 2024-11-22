using JGToolbar.Helper;
using System.Windows;

namespace JGToolbar
{
    public class WindowPositionManager
    {
        private readonly Window window;
        private double smoothingFactor = 0.5;

        public WindowPositionManager(Window window)
        {
            this.window = window;
        }

        /// <summary>
        /// Position the window near the File Explorer window.
        /// </summary>
        public void PositionNearExplorer()
        {
            IntPtr explorerHandle = WindowApi.FindWindow("CabinetWClass", null);

            // Check if the active window is not File Explorer 
            IntPtr foregroundHandle = WindowApi.GetForegroundWindow();
            if (foregroundHandle != explorerHandle ||
                explorerHandle == IntPtr.Zero ||
                ControlPanelHelper.IsControlPanelOrNested(explorerHandle) ||
                IsWindowMinimized(explorerHandle))
            {
                window.Hide();
                return;
            }

            window.Show();

            WindowApi.RECT rect;

            if (WindowApi.GetWindowRect(explorerHandle, out rect))
            {
                // Calculate target positions for bottom-center alignment
                double targetLeft = rect.Left + (rect.Right - rect.Left) / 2 - (window.Width / 2);
                double targetTop = rect.Bottom - window.Height - 10;

                // Smooth movement using linear interpolation
                window.Left += (targetLeft - window.Left) * smoothingFactor;
                window.Top += (targetTop - window.Top) * smoothingFactor;

                // Snap to target position if close enough
                if (Math.Abs(targetLeft - window.Left) < 1 && Math.Abs(targetTop - window.Top) < 1)
                {
                    window.Left = targetLeft;
                    window.Top = targetTop;
                }
            }
        }

        /// <summary>
        /// Check if a window is minimized.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        private bool IsWindowMinimized(IntPtr hWnd)
        {
            return WindowApi.IsIconic(hWnd);
        }
    }
}
