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
        /// Supports nested keys using "." as a separator. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyPath"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public static T GetSettingValue<T>(string keyPath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("AppSettings.json not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new FileNotFoundException("AppSettings.json not found.");
            }

            string json = File.ReadAllText(filePath);

            try
            {
                JsonNode? rootNode = JsonNode.Parse(json);
                if (rootNode == null)
                {
                    MessageBox.Show("The JSON file is empty or invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new InvalidDataException("The JSON file is empty or invalid.");
                }

                // Split the keyPath by "." to support nested keys
                string[] keys = keyPath.Split('.');

                JsonNode? currentNode = rootNode;
                foreach (string key in keys)
                {
                    currentNode = currentNode?[key];
                    if (currentNode == null)
                    {
                        MessageBox.Show($"Key path '{keyPath}' not found in AppSettings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw new KeyNotFoundException($"Key path '{keyPath}' not found in AppSettings.");
                    }
                }
                return currentNode.Deserialize<T>()!;
            }
            catch (JsonException ex)
            {
                MessageBox.Show("Error parsing AppSettings.json.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new InvalidDataException("Error parsing AppSettings.json.", ex);
            }
        }

    }
}
