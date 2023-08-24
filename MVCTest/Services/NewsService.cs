using MVCTest.Models;
using Newtonsoft.Json;
using Polly.Retry;
using Polly;
using System.Net;

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

        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy = Policy
        .Handle<HttpRequestException>()
        .OrResult<HttpResponseMessage>(response => response.StatusCode == HttpStatusCode.BadRequest)
        .WaitAndRetryAsync(new[]
        {
            TimeSpan.FromSeconds(1),
            TimeSpan.FromSeconds(2),
            TimeSpan.FromSeconds(4)
        }, (result, timeSpan, retryCount, context) =>
        {
            if (result.Exception != null)
            {
                // Log error to the console
                Console.WriteLine($"Retry attempt {retryCount} failed due to: {result.Exception.Message}");
            }
            else if (result.Result != null)
            {
                // Log response details to the console
                var response = result.Result;
                Console.WriteLine($"Retry attempt {retryCount} failed with response: {response.StatusCode} - {response.ReasonPhrase}");
            }
        });


        public async Task<NewsApiResponse> GetLatestNewsAsync(string country)
        {
            await Task.Delay(1000);
            var url = $"{BaseUrl}?country={country}&apiKey={_apiKey}";



            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.GetAsync(url));

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
