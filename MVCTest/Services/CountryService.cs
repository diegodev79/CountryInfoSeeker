using Microsoft.AspNetCore.Mvc.Rendering;
using MVCTest.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVCTest.Services
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _httpClient;
        private List<CountryModel> _countries;

        public CountryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task LoadCountriesAsync()
        {
            var response = await _httpClient.GetAsync("https://restcountries.com/v3.1/all?fields=name,cca2,cca3");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                _countries = JsonSerializer.Deserialize<List<CountryModel>>(content);                            

            }
        }

        public async Task<List<SelectListItem>> GetCountrySelectListAsync()
        {
            await LoadCountriesAsync(); // Ensure the countries are loaded

            List<SelectListItem> selectListItems = _countries
                .Select(c => new SelectListItem(c.name.common, c.cca2))
                .ToList();

            return selectListItems;
        }

        public IReadOnlyList<CountryModel> GetCountries()
        {
            return _countries;
        }
    }

}