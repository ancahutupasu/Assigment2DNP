﻿using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Models;

namespace Assigment2.Persistence
{
    public class FileContext
    {
        private readonly string adultsFile = "adults.json";

        public FileContext()
        {
            Adults = File.Exists(adultsFile) ? ReadData<Adult>(adultsFile) : new List<Adult>();
        }

        public IList<Adult> Adults { get; }

        private IList<T> ReadData<T>(string fileName)
        {
            using (var jsonReader = File.OpenText(fileName))
            {
                return JsonSerializer.Deserialize<List<T>>(jsonReader.ReadToEnd());
            }
        }

        public void SaveChanges()
        {
            var jsonAdults = JsonSerializer.Serialize(Adults, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (var outputFile = new StreamWriter(adultsFile, false))
            {
                outputFile.Write(jsonAdults);
            }
        }
    }
}