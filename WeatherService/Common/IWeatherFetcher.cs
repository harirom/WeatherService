using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService.Common
{
   public interface IWeatherFetcher
    {
        CurrentWeather GetCurrentWeather(string CityId);
    }
}
