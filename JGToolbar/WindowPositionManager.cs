using System.Runtime.InteropServices;
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
            IntPtr explorerHandle = FindWindow("CabinetWClass", null);

            // Check if the active window is not File Explorer 
            IntPtr foregroundHandle = GetForegroundWindow();
            if (foregroundHandle != explorerHandle || explorerHandle == IntPtr.Zero)
            {
                window.Hide();
                return;
            }

            if (IsWindowMinimized(explorerHandle))
            {
                window.Hide();
                return;
            }

            window.Show();

            RECT rect;

            if (GetWindowRect(explorerHandle, out rect))
            {
                double targetLeft = rect.Left + (rect.Right - rect.Left) / 2 - (window.Width / 2);
                double targetTop = rect.Bottom - window.Height - 10;

                window.Left += (targetLeft - window.Left) * smoothingFactor;
                window.Top += (targetTop - window.Top) * smoothingFactor;

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
            return IsIconic(hWnd);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
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
