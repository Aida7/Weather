using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast
{
    public class WindInfo
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
    }
}