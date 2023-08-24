using MVCTest.Models;
using Newtonsoft.Json;

namespace MVCTest.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.coincap.io/v2/assets";

        public CurrencyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CurrencyModel> GetCurrencyToBitcoinAsync(string currencyCode)
        {
            string url = $"{BaseUrl}/rates/bitcoin?convert={currencyCode}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var currencyData = JsonConvert.DeserializeObject<CurrencyModel>(content);

                return currencyData;
            }

            // Handle error cases here
            return new CurrencyModel();
        }
    }

    
}
