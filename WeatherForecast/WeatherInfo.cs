using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast
{
    public class WeatherInfo
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }
}