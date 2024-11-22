using System.Diagnostics;

namespace JGToolbar.Helper
{
    public class ProcessHelper
    {
        /// <summary>
        /// Get the process name by the process ID.
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static string GetProcessNameById(int processId)
        {
            try
            {
                using (var process = Process.GetProcessById(processId))
                {
                    return process.ProcessName;
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
