using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WeatherForecast
{
    public class WeatherModel
    {
        public DateTime DayWeather { get; set; }
        public Uri ImageWeatherUrl { get; set; }
        public List<double> DayTemperatures { get; set; }
        public List<double> DayWinds { get; set; }
        public List<double> DayHumidityInfo { get; set; }
        public WeatherModel()
        {
            DayTemperatures = new List<double>();
            DayWinds = new List<double>();
            DayHumidityInfo = new List<double>();
        }

        public void InsertData(double temp, double wind, double humidity)
        {
            DayTemperatures.Add(temp);
            DayWinds.Add(wind);
            DayHumidityInfo.Add(humidity);
        }
    }
}