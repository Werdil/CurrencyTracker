using CurrencyTracker.Domain.Entities;

namespace CurrencyTracker.Domain.Interfaces
{
    public interface ICurrencyHelper
    {
        Task<Currency> GetCurrency(string code);
    }
}
