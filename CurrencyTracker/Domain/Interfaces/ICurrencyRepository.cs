using CurrencyTracker.Domain.Entities;

namespace CurrencyTracker.Domain.Interfaces;
public interface ICurrencyRepository
{
    Task<Currency> GetByCodeAsync(string code);
    Task<List<Currency>> GetAllAsync();
    Task AddAsync(Currency currency);
    Task UpdateAsync(Currency currency);
    Task RemoveByCodeAsync(string code);
}
