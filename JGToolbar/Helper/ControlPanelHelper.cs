namespace JGToolbar.Helper
{
    public class ControlPanelHelper
    {
        /// <summary>
        /// Check if the window title contains any of the specified patterns.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="patterns"></param>
        /// <returns></returns>
        public static bool IsControlPanelTitleOrHierarchy(string title, List<string> patterns)
        {
            if (string.IsNullOrEmpty(title))
            {
                return false;
            }

            foreach (string pattern in patterns)
            {
                if (title.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if the window is a Control Panel window or a nested window.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static bool IsControlPanelOrNested(IntPtr hWnd)
        {
            uint processId;
            WindowApi.GetWindowThreadProcessId(hWnd, out processId);

            string processName = ProcessHelper.GetProcessNameById((int)processId);
            if (!processName.Equals("explorer", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            string title = WindowApi.GetWindowText(hWnd);
            List<string> controlPanelPatterns = ConfigurationHelper.GetSettingValue<List<string>>("ControlPanelPatterns");
            return IsControlPanelTitleOrHierarchy(title, controlPanelPatterns);
        }
    }
}
