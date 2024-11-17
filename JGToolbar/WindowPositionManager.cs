using System;
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
            if (explorerHandle != IntPtr.Zero)
            {
                if (IsWindowMinimized(explorerHandle))
                {
                    window.Hide();
                    return;
                }
                else
                {
                    window.Show();
                }

                RECT rect;
                if (GetWindowRect(explorerHandle, out rect))
                {
                    // Calculate target positions for bottom-center alignment
                    double targetLeft = rect.Left + (rect.Right - rect.Left) / 2 - (window.Width / 2); // Center horizontally
                    double targetTop = rect.Bottom - window.Height - 10; // Position slightly above bottom edge (adjust margin)

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
            else
            {
                window.Hide();
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
