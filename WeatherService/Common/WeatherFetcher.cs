using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WeatherService.Common
{
    public class WeatherFetcher : IWeatherFetcher
    {
        private HttpClient client = new HttpClient();

        private string APIUrl;
        private string APIKey;

        public WeatherFetcher(string APIUrl, string APIKey)
        {

            this.APIUrl = APIUrl;
            this.APIKey = APIKey;
        }

        public CurrentWeather GetCurrentWeather(string CityId)
        {
            var json = RunAsync(APIKey, CityId).GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<CurrentWeather>(json);
        }

       
        private async Task<string> RunAsync(string key, string CityId)
        {            
            client.BaseAddress = new Uri(APIUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = "";
            try
            {
                result = await GetWeatherAsync(key, CityId);
            }
            catch (Exception e)
            { 
                Console.WriteLine(e.Message); // This need to be taken care off. 
            }

            return result;
        }

        private async Task<string> GetWeatherAsync(string key, string CityId)
        {
            var result = "";
            string url = $"/data/2.5/weather?id={CityId}&appid={key}";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }
    }
}
