namespace devops;

using System;
using System.IO;
using System.Collections.Generic;

    public class DotEnv
    {
        private string filePath;
        private Dictionary<string, string> variables = new Dictionary<string, string>();

        public DotEnv(string filePath)
        {
            this.filePath = filePath;
            Load();
        }

        public string Get(string key)
        {
            if (!variables.ContainsKey(key))
            {
                throw new KeyNotFoundException($"Key '{key}' not found in .env file");
            }

            return variables[key];
        }

        private void Load()
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File '{filePath}' not found");
            }

            foreach (string line in File.ReadAllLines(filePath))
            {
                if (line.StartsWith("#"))
                {
                    continue; // Ignore comments
                }

                string[] parts = line.Split('=');
                if (parts.Length != 2)
                {
                    throw new FormatException($"Invalid format of line '{line}'");
                }

                string key = parts[0].Trim();
                string value = parts[1].Trim();

                variables[key] = value;
            }
        }
    }
