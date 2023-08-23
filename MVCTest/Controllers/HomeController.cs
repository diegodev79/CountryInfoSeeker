using Microsoft.AspNetCore.Mvc;
using MVCTest.Models;
using MVCTest.Services;
using System.Diagnostics;

namespace MVCTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private readonly ICountryInfoService _countryInfoService;
        private readonly INewsService _newsService;

        public HomeController(ILogger<HomeController> logger, ICountryInfoService countryInfoService, INewsService newsService)
        {
            _logger = logger;
            _countryInfoService = countryInfoService;
            _newsService = newsService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            CountryInfoModel countryInfoModel = await _countryInfoService.GetCountryInfoAsync("ARG");
            NewsApiResponse newsApiResponse = await _newsService.GetLatestNewsAsync("AR");
            HomeViewModel homeViewModel = new HomeViewModel
            {
                //WeatherModel = weatherModel,
                CountryInfoModel = countryInfoModel,
                NewsApiResponse = newsApiResponse
                //CurrencyModel = currencyModel
            };

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}