﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IWeatherService _weatherService;
        private readonly ICurrencyService _currencyService;
        private readonly ICountryService _countryService;
      

        public HomeController(ILogger<HomeController> logger, ICountryInfoService countryInfoService, 
            INewsService newsService, IWeatherService weatherService, ICurrencyService currencyService, ICountryService countryService)
        {
            _logger = logger;
            _countryInfoService = countryInfoService;
            _newsService = newsService;
            _weatherService = weatherService;
            _currencyService = currencyService;
            _countryService = countryService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            CountryInfoModel countryInfoModel = await _countryInfoService.GetCountryInfoAsync("ARG");
            NewsApiResponse newsApiResponse = await _newsService.GetLatestNewsAsync("AR");
            WeatherModel weatherModel = await _weatherService.GetWeatherAsync("Bahia Blanca", "Argentina");
            CurrencyModel currencyModel = await _currencyService.GetCurrencyToBitcoinAsync("ars");

            HomeViewModel homeViewModel = new HomeViewModel
            {
                WeatherModel = weatherModel,
                CountryInfoModel = countryInfoModel,
                NewsApiResponse = newsApiResponse,
                CurrencyModel = currencyModel,
                CountrySelectList = await _countryService.GetCountrySelectListAsync()
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