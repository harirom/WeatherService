using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WeatherService.Common
{
    public static class FileIOHelper
    {
        /// <summary>
        /// Create folder hierarchy
        /// </summary>
        public static DirectoryInfo CreateDestinationFolder(string baseFolder)
        {
            var now = DateTime.Now;
            var yearName = now.ToString("yyyy");
            var monthName = now.ToString("MMMM");
            var dayName = now.ToString("dd-MM-yyyy");

            var folder = Path.Combine(baseFolder,
                           Path.Combine(yearName,
                             Path.Combine(monthName,
                               dayName)));

            return Directory.CreateDirectory(folder);
        }
        /// <summary>
        /// Convert list object into CSV file
        /// </summary>
        public static void WriteToCSV<T>(IEnumerable<T> items, string path)
        {
            Type itemType = typeof(T);
            var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .OrderBy(p => p.Name);

            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine(string.Join(", ", props.Select(p => p.Name)));

                foreach (var item in items)
                {
                    writer.WriteLine(string.Join(", ", props.Select(p => p.GetValue(item, null))));
                }
            }
        }
        /// <summary>
        /// Convert list object into CSV file
        /// </summary>
        public static void WriteToCSV<T>(T item, string filePath)
        {
            Type itemType = typeof(T);
            var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .OrderBy(p => p.Name);

            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine(string.Join(", ", props.Select(p => p.Name)));
                writer.WriteLine(string.Join(", ", props.Select(p => p.GetValue(item, null))));                
            }
        }

        /// <summary>
        /// Writes the given object instance to a Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [JsonIgnore] attribute.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// Reads an object instance from an Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the Json file.</returns>
        public static T ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        /// <summary>
        /// Read file and return cities
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IEnumerable<City> ReadFromFile(string filePath) 
        {
            if (!File.Exists(filePath)) return null;
            
            char[] delimiters = new char[] { '=' };
            var fileLines = File.ReadLines(filePath)
                                .Select(line =>
                                {
                                    string[] ss = line.Split(delimiters);
                                    return (ss.Length == 2)
                                   ? new City() { CityId = ss[0], Cityname = ss[1] }
                                   : null;              
                            })
                                .Where(x => x != null)      
                                .ToList();
            return fileLines;
        }
    }
}
