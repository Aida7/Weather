using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeatherForecast
{
    public partial class MainWindow : Window
    {
        
        public List<City> CityList { get; set; }
        public CityMainWeather CityMainWeather { get; set; }
        public List<WeatherModel> WeatherModelUIs { get; set; }
        public int MaxItemsIn { get; set; }
        public string CurrentCity { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MaxItemsIn = 5;
        }

        public void Refresh()
        {
            string selectedCity = citySelector.Text.Substring(0, 1).ToUpper() + citySelector.Text.Substring(1, citySelector.Text.Length - 1);

            if (!CityList.Any(c => c.Name == selectedCity))
            {
                MessageBox.Show("error");
                return;
            }

            WeatherModelUIs = new List<WeatherModel>();

            RequestWeatherInfo(selectedCity);
        }
        
        public void RefreshUI(object sender, DownloadStringCompletedEventArgs e)
        {
            MessageBox.Show("Weather in " + CurrentCity + " for " + MaxItemsIn.ToString() + " days");
            for (int i = 0; i < MaxItemsIn; i++)
            {
                ((TextBlock)((StackPanel)mainStackPanel.Children[i]).Children[0]).Text = WeatherModelUIs[i].DayWeather.Date.Day.ToString() + " " + CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(WeatherModelUIs[i].DayWeather.Date.Month);
                ((Image)((StackPanel)mainStackPanel.Children[i]).Children[1]).Source = new BitmapImage(WeatherModelUIs[i].ImageWeatherUrl);
                ((TextBlock)((StackPanel)mainStackPanel.Children[i]).Children[2]).Text = WeatherModelUIs[i].DayTemperatures.Average().ToString() + " C";
                ((TextBlock)((StackPanel)mainStackPanel.Children[i]).Children[3]).Text = WeatherModelUIs[i].DayWinds.Average().ToString() + " m/s";
                ((TextBlock)((StackPanel)mainStackPanel.Children[i]).Children[4]).Text = WeatherModelUIs[i].DayHumidityInfo.Average().ToString() + " %";
            }
        }

        public void RequestWeatherInfo(string city)
        {
            var searchCityId = CityList.Where(c => c.Name == city).Select(c => c.Id).FirstOrDefault();

            string url = @"https://api.openweathermap.org/data/2.5/forecast?units=metric&APPID=eb5e75d2643132a6c84e9c510ec6219e&id=";

            using (var client = new WebClient())
            {
                client.DownloadStringAsync(new Uri(url + searchCityId));
                client.DownloadStringCompleted += ClientDownloadStringCompleted;
                client.DownloadStringCompleted += RefreshUI;
            }
        }

        private void ClientDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            CityMainWeather = JsonConvert.DeserializeObject<CityMainWeather>(e.Result);
            CurrentCity = CityMainWeather.City.Name;

            foreach (var weatherInfo in CityMainWeather.Forecasts)
            {
                if (WeatherModelUIs.Count == 0 || WeatherModelUIs.Last().DayWeather < weatherInfo.ForecastTime.Date)
                {
                    WeatherModel newWeatherInfo = new WeatherModel();
                    string newIconUrl = @"http://openweathermap.org/img/w/" + weatherInfo.ShortInfo.Last().IconName + ".png";
                    newWeatherInfo.DayWeather = weatherInfo.ForecastTime;
                    newWeatherInfo.ImageWeatherUrl = new Uri(newIconUrl);
                    newWeatherInfo.InsertData(weatherInfo.MainInfo.Temperature, weatherInfo.WindInfo.Speed, weatherInfo.MainInfo.Humidity);
                    WeatherModelUIs.Add(newWeatherInfo);
                }
                else
                {
                    if (weatherInfo.ForecastTime.Hour == 12)
                    {
                        string newIconUrl = @"http://openweathermap.org/img/w/" + weatherInfo.ShortInfo.Last().IconName + ".png";
                        WeatherModelUIs.Last().ImageWeatherUrl = new Uri(newIconUrl);
                    }
                    WeatherModelUIs.Last().InsertData(weatherInfo.MainInfo.Temperature, weatherInfo.WindInfo.Speed, weatherInfo.MainInfo.Humidity);
                }
            }
        }

        private void SearchClicked(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
