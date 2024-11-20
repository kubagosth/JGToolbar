using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Forms;

namespace JGToolbar.Helper
{
    public static class ConfigurationHelper
    {
        private static string filePath = "Settings/AppSettings.json";

        /// <summary>
        /// Get a setting value from the AppSettings.json file. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        public static T GetSettingValue<T>(string key)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("AppSettings.json not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new FileNotFoundException("AppSettings.json not found.");
            }


            string json = File.ReadAllText(filePath);

            try
            {
                JsonNode rootNode = JsonNode.Parse(json);
                JsonNode? valueNode = rootNode?[key];

                if (valueNode == null)
                {
                    MessageBox.Show($"Key '{key}' not found in AppSettings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new KeyNotFoundException($"Key '{key}' not found in AppSettings.");
                }

                return valueNode.Deserialize<T>();
            }
            catch (JsonException ex)
            {
                MessageBox.Show("Error parsing AppSettings.json.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new InvalidDataException("Error parsing AppSettings.json.", ex);
            }
        }
    }
}
