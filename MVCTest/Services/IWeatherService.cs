using MultiInfoMVC.Models;

namespace MultiInfoMVC.Services
{
    public interface IWeatherService
    {
        Task<WeatherModel> GetWeatherAsync(string city, string country);
    }
}
