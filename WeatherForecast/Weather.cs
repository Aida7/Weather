using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast
{
    public class Weather
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("main")]
        public string ShortInfo { get; set; }
        [JsonProperty("icon")]
        public string IconName { get; set; }
    }
}