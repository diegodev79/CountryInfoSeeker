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
            var url = $"{BaseUrl}/{currencyCode}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var currencyData = JsonConvert.DeserializeObject<CurrencyApiResponse>(content);

                if (currencyData?.Data != null)
                {
                    var btcEquivalent = currencyData.Data.PriceBtc;

                    return new CurrencyModel
                    {
                        CurrencyCode = currencyCode,
                        BtcEquivalent = btcEquivalent
                    };
                }
            }

            // Handle error cases here
            return new CurrencyModel();
        }
    }

    // This class represents the structure of the response from the CoinCap API
    public class CurrencyApiResponse
    {
        public CurrencyData Data { get; set; }
    }

    public class CurrencyData
    {
        [JsonProperty("priceBtc")]
        public decimal PriceBtc { get; set; }
    }

}
