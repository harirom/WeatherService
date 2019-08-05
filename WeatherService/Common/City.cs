using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService.Common
{
   public class FileInfo
    {
        public String FileName { get; set; }
        public List<City> CityList { get; set; }        
    }
    public class City
    {
        public string Cityname { get; set; }
        public string CityId { get; set; }

    }
}
