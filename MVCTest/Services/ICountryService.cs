using Microsoft.AspNetCore.Mvc.Rendering;
using MVCTest.Models;

namespace MVCTest.Services
{
    public interface ICountryService
    {
        Task LoadCountriesAsync();
        Task<List<SelectListItem>> GetCountrySelectListAsync();
        IReadOnlyList<CountryModel> GetCountries();
    }
}
