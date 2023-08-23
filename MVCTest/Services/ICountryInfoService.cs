using MVCTest.Models;

namespace MVCTest.Services
{
    public interface ICountryInfoService
    {
        Task<CountryInfoModel> GetCountryInfoAsync(string countryName);
    }
}
