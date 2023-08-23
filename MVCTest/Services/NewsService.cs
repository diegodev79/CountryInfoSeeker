using MVCTest.Models;
using Newtonsoft.Json;

namespace MVCTest.Services
{
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string BaseUrl = "https://newsapi.org/v2/top-headlines";

        public NewsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["NewsApi:ApiKey"];
        }
       
        public async Task<NewsApiResponse> GetLatestNewsAsync(string country)
        {
            var url = $"{BaseUrl}?country={country}&apiKey={_apiKey}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var newsData = JsonConvert.DeserializeObject<NewsApiResponse>(content);

                // Assuming NewsApiResponse contains an array of articles and you extract the first one
                return newsData;
            }

            // Handle error cases here
            return new NewsApiResponse();
        }
    }

    // This class represents the structure of the response from the news API
   
}
