using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherService.Common;
using WeatherService.Models;
using Microsoft.Extensions.Configuration;

namespace WeatherService.Controllers
{
    public class HomeController : Controller
    {
        IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var destPath = _configuration["DestinationDir"];
            
            using (var stream = new FileStream(file.FileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            IEnumerable<City> cities = FileIOHelper.ReadFromFile(file.FileName);
          
            Parallel.ForEach(cities, (currentCity) =>
            {
                IWeatherFetcher wf = new WeatherFetcher(_configuration["WeatherAPIUrl"], _configuration["WeatherAPIKey"]);
                var currentWeather = wf.GetCurrentWeather(currentCity.CityId);
                var dirInfo = FileIOHelper.CreateDestinationFolder(destPath);
                FileIOHelper.WriteToJsonFile<CurrentWeather>($"{dirInfo.FullName}\\{currentCity.Cityname}_{currentCity.CityId} .txt", currentWeather, append: false);
            });
           
            ViewBag.Message = string.Format("Report generated at specified destination path.\\nCurrent Date and Time: {0}", DateTime.Now.ToString());
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
