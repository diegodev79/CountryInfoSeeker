using MVCTest.Models;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace MVCTest.Services
{
    public class CountryInfoService : ICountryInfoService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://restcountries.com/v3.1/alpha/";

        public CountryInfoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CountryInfoModel> GetCountryInfoAsync(string conutryCode)
        {
            string fields = "?fields=name,capital,currencies,area,flag,languages,latlng,population,independent";
            var response = await _httpClient.GetAsync($"{BaseUrl}{conutryCode}{fields}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                //var countryInfo = JsonConvert.DeserializeObject<CountryInfoModel[]>(content);
                var countryInfo = JsonConvert.DeserializeObject<CountryInfoModel>(content);
                // Assuming the response contains an array of country info, and you extract the first element
                return countryInfo;
            }

            // Handle error cases here
            return null;
        }
    }
}
