
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast
{
    public class CityMainWeather
    {
        [JsonProperty("cod")]
        public string ResponseCode { get; set; }
        [JsonProperty("message")]
        public double ResponseMessage { get; set; }
        [JsonProperty("list")]
        public List<ForecastWeather> Forecasts { get; set; }
        [JsonProperty("city")]
        public City City { get; set; }
    }
}