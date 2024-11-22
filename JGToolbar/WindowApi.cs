using System.Runtime.InteropServices;
using System.Text;

namespace JGToolbar
{
    public static class WindowApi
    {
        [DllImport("user32.dll")]
        public static extern nint FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern nint GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(nint hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern bool IsIconic(nint hWnd);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(nint hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(nint hWnd, StringBuilder lpString, int nMaxCount);

        public static string GetWindowText(nint hWnd)
        {
            const int nChars = 256;
            StringBuilder windowText = new StringBuilder(nChars);
            GetWindowText(hWnd, windowText, nChars);
            return windowText.ToString();
        }

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