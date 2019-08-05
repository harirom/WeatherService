using NUnit.Framework;
using WeatherService.Common;
namespace WeatherService.Tests
{
    //[TestClass()]
    [TestFixture(TestName = "WeatherFetcherTest")]
    public class WeatherFetcherTests
    {
        IWeatherFetcher wf = new WeatherFetcher("http://api.openweathermap.org", "aa69195559bd4f88d79f9aadeb77a8f6");

        //NameValueCollection appSettings = ConfigurationManager.AppSettings;
        //[TestMethod()]
        [TestCase(TestName = "GetCurrentWeatherTest")]
        public void GetCurrentWeatherTest()
        {          
            var currentWeather = wf.GetCurrentWeather("2643741");
            Assert.IsNotNull(currentWeather, $"The temp in {currentWeather.name} is {currentWeather.main.temp}.");
        }
    }
}