using MultiInfoMVC.Models;

namespace MultiInfoMVC.Services
{
    public interface ICurrencyService
    {
        Task<CurrencyModel> GetCurrencyToBitcoinAsync(string currencyCode);
    }
}
