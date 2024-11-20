using JGToolbar.Settings;
using System.IO;
using System.Text.Json;

namespace JGToolbar.Helper
{
    public static class ConfigurationHelper
    {
        public static AppSettings LoadSettings(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("AppSettings.json not found.");

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<AppSettings>(json);
        }
    }
}
