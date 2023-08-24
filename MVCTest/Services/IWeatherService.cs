using MVCTest.Models;

namespace MVCTest.Services
{
    public interface IWeatherService
    {
        Task<WeatherModel> GetWeatherAsync(string city, string country);
    }
}
