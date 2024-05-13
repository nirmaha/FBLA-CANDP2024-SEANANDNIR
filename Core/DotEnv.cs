using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace EduPartners.Core
{
    public static class DotEnv
    {
        public static string Load(string variableName)
        {
            // Name of the embedded resource file containing environment variables
            string resourceFileName = "EduPartners..env";

            // Load the embedded resource
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceFileName))
            {
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        // Read the content of the embedded resource
                        string content = reader.ReadToEnd();

                        // Split the content into lines
                        string[] lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                        // Parse each line as key-value pairs and set environment variables
                        foreach (string line in lines)
                        {
                            // Split each line into key-value pair
                            string[] parts = line.Split('=');

                            if (parts.Length == 2)
                            {
                                // Set environment variable
                                Environment.SetEnvironmentVariable(parts[0], parts[1]);
                            }
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("Resource file not found.");
                    return "";
                }
            }

            // Now you can access the environment variables
            return Environment.GetEnvironmentVariable(variableName);
        }
    }
}
