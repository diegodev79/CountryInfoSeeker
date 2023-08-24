﻿using MultiInfoMVC.Models;
using Newtonsoft.Json;

namespace MultiInfoMVC.Services
{
    public class WeatherService : IWeatherService
    {
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["WeatherApi:ApiKey"];
        }

        public async Task<WeatherModel> GetWeatherAsync(string city, string country)
        {
            var url = $"{BaseUrl}?q={city},{country}&appid={_apiKey}";
            
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var weatherData = JsonConvert.DeserializeObject<WeatherApiResponse>(content);

                if (weatherData != null)
                {
                    var weatherModel = new WeatherModel
                    {
                        Temperature = weatherData.Main.Temperature,
                        Description = weatherData.Weather[0].Description
                    };
                    return weatherModel;
                }
            }
            
            // Handle error cases here
            return null;
        }
    }

    // This class represents the structure of the response from the weather API
    public class WeatherApiResponse
    {
        public WeatherMain Main { get; set; }
        public WeatherDescription[] Weather { get; set; }
    }

    public class WeatherMain
    {
        public float Temperature { get; set; }
    }

    public class WeatherDescription
    {
        public string Description { get; set; }
    }
}
