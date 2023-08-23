using MVCTest.Models;

namespace MVCTest.Services
{
    public interface INewsService
    {
        Task<NewsApiResponse> GetLatestNewsAsync(string country);
    }
}
