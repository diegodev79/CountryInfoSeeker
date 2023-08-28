using MVCTest.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVCTest.Services
{
    public class CountryService
    {
        private readonly HttpClient _httpClient;
        private List<CountryModel> _countries;

        public CountryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task LoadCountriesAsync()
        {
            var response = await _httpClient.GetAsync("https://restcountries.com/v3/all");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                _countries = JsonSerializer.Deserialize<List<CountryModel>>(content);
            }
        }

        public IReadOnlyList<CountryModel> GetCountries()
        {
            return _countries;
        }
    }
}