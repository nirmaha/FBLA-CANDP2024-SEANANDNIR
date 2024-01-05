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

        public IniFile(string argPath)
        {
            filePath = argPath;

            sections = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

            Load();
        }

        public void Load()
        {
            sections.Clear();

            string currentSection = string.Empty;

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

        public string GetValue(string section, string key, string defaultValue = "")
        {
            if (sections.TryGetValue(section, out Dictionary<string, string> values) && values.TryGetValue(key, out string value))
            {
                return value;
            }

            return defaultValue;
        }

        public void SetValue(string section, string key, string value)
        {
            if (!sections.TryGetValue(section, out Dictionary<string, string> values))
            {
                values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                sections[section] = values;
            }

            values[key] = value;
        }

        public List<string> GetSectionNames()
        {
            return sections.Keys.ToList();
        }
    }

}