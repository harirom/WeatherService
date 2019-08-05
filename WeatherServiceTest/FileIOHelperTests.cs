using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using WeatherService.Common;
using NUnit.Framework;

namespace WeatherService.Tests
{
    //[TestClass()]
    [TestFixture(TestName = "FileIOHelperTest")]
    public class UtilityHelperTests
    {
        //[TestMethod()]
        [TestCase(TestName = "ReadFromFileCheckIfFiledoesnotExistTest")]
        public void ReadFromFileCheckIfFiledoesnotExistTest()
        {
            var filepath = @"D:\\Weather Info\\Destination";
            var cities = FileIOHelper.ReadFromFile(filepath);
            Assert.IsNull(cities);
        }

        //[TestMethod()]
        [TestCase(TestName = "ReadFromFileTest")]
        public void ReadFromFileTest()
        {
            var filepath = @"D:\\Weather Info\\Source\CityList.txt";
            IEnumerable<City> cities = FileIOHelper.ReadFromFile(filepath);
            Assert.AreEqual(10, cities.Count());

        }

        //[TestMethod()]
        [TestCase(TestName = "CreateDestinationFolder")]
        public void CreateDestinationFolder()
        {
            string path = "D:\\Weather Info\\Source";
            var directoryInfo = FileIOHelper.CreateDestinationFolder(path);
            var now = DateTime.Now;
            var yearName = now.ToString("yyyy");
            var monthName = now.ToString("MMMM");
            var dayName = now.ToString("dd-MM-yyyy");
            var doesFileExits = Directory.Exists($"{path}\\{yearName}\\{monthName}\\{dayName}");
            Assert.IsTrue(doesFileExits, "Folder created successfully.");

        }

       // [TestMethod()]
       [TestCase(TestName = "WriteToJsonFileTest")]
        public void WriteToJsonFileTest()
        {
            string path = "D:\\Weather Info\\Source";
            var directoryInfo = FileIOHelper.CreateDestinationFolder(path);
            var now = DateTime.Now;
            var yearName = now.ToString("yyyy");
            var monthName = now.ToString("MMMM");
            var dayName = now.ToString("dd-MM-yyyy");
            string filepath = $"{path}\\{yearName}\\{monthName}\\{dayName}";


            City city = new City() { Cityname = "London", CityId = "2643741" };

            IWeatherFetcher wf = new WeatherFetcher("http://api.openweathermap.org", "aa69195559bd4f88d79f9aadeb77a8f6");
            var currentWeather = wf.GetCurrentWeather(city.CityId);
            var destinationFilepath = $"{filepath}\\{city.Cityname}_{city.CityId}.txt";
            FileIOHelper.WriteToJsonFile<CurrentWeather>(destinationFilepath, currentWeather, append: false);

            var DoesFileExist=File.Exists(destinationFilepath);

            Assert.IsTrue(DoesFileExist);
        }
    }
}