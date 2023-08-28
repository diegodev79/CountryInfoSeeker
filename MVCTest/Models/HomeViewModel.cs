using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCTest.Models
{
    public class HomeViewModel
    {
        public WeatherModel WeatherModel { get; set; }
        public CountryInfoModel CountryInfoModel { get; set; }
        public NewsApiResponse NewsApiResponse { get; set; }
        public CurrencyModel CurrencyModel { get; set; }
        //public SelectList CountrySelectList { get; set; }
        public string SelectedCountry { get; set; }
        public List<SelectListItem> CountrySelectList { get; set; }
    }
}
