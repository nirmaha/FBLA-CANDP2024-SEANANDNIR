using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EduPartners.Core
{
    public class IniFile
    {
        private readonly Dictionary<string, Dictionary<string, string>> sections;

        private string filePath = "";
        private string directoryPath = "";

        /// <summary>
        /// This class manipulates the Ini file in <paramref name="argPath"/>.
        /// </summary>
        /// <param name="argPath">The full path to the file.</param>
        /// <param name="directoryPath">The path that contains the file.</param>
        public IniFile(string argPath, string directoryPath)
        {
            filePath = argPath;
            this.directoryPath = directoryPath;

            sections = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

            Load();
        }

       /// <summary>
       /// The Load function reads a configuration file, parses it into sections.
       /// </summary>
        public void Load()
        {
            sections.Clear();

            string currentSection = string.Empty;

            if (!Directory.Exists(directoryPath))
            { 
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(filePath))
            {
                FileStream file = File.Create(filePath);
                file.Close();
            }

            foreach (string line in File.ReadLines(filePath))
            {
                if (line.Trim().StartsWith("[") && line.Trim().EndsWith("]"))
                {
                    currentSection = line.Trim('[', ']');
                    sections[currentSection] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }
                else if (!string.IsNullOrWhiteSpace(currentSection))
                {
                    string[] parts = line.Split(new[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();
                        sections[currentSection][key] = value;
                    }
                }
            }
        }


        /// <summary>
        /// This function saves the writes to the current Ini File.
        /// </summary>
        public void Save()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (KeyValuePair<string, Dictionary<string, string>> section in sections)
                {
                    writer.WriteLine($"[{section.Key}]");
                    foreach (KeyValuePair<string, string> entry in section.Value)
                    {
                        writer.WriteLine($"{entry.Key}={entry.Value}");
                    }
                    writer.WriteLine();
                }
            }
        }

        /// <summary>
        /// This function gets the value from the specified key in the specified section.
        /// </summary>
        /// <param name="section">The section that the <paramref name="key"/> is in.</param>
        /// <param name="key">The name the value is assigned to.</param>
        /// <returns>The value of the <paramref name="key"/> or nothing if getting the value fails.</returns>
        public string GetValue(string section, string key)
        {
            if (sections.TryGetValue(section, out Dictionary<string, string> values) && values.TryGetValue(key, out string value))
            {
                return value;
            }

            return "";
        }

        /// <summary>
        /// This function sets value to a specified <paramref name="key"/> in a <paramref name="section"/>.
        /// </summary>
        /// <param name="section">The section that the <paramref name="key"/> is in.</param>
        /// <param name="key">The name the value is assigned to.</param>
        /// <param name="value">The value to set to the <paramref name="key"/>.</param>
        public void SetValue(string section, string key, string value)
        {
            if (!sections.TryGetValue(section, out Dictionary<string, string> values))
            {
                values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                sections[section] = values;
            }

            values[key] = value;
        }

        /// <summary>
        /// This function gets all of the sections in a file.
        /// </summary>
        /// <returns>A list of sections.</returns>
        public List<string> GetSectionNames()
        {
            return sections.Keys.ToList();
        }
    }

}