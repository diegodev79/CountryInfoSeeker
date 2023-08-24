using MVCTest.Models;

namespace MVCTest.Services
{
    public interface ICurrencyService
    {
        Task<CurrencyModel> GetCurrencyToBitcoinAsync(string currencyCode);
    }
}
