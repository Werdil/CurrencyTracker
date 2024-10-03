using CurrencyTracker.Domain.Entities;
using CurrencyTracker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CurrencyTracker.Infrastructure.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly ApplicationContext _context;

    public CurrencyRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Currency> GetWithExchangeRatesByCodeAsync(string code)
    {
        return await _context.Currencies
            .Include(c => c.ExchangeRates)
            .FirstOrDefaultAsync(c => c.Code == code);
    }
    public async Task<Currency> GetByCodeAsync(string code)
    {
        return await _context.Currencies
            .FirstOrDefaultAsync(c => c.Code == code);
    }
    public async Task<List<Currency>> GetAllAsync()
    {
        return await _context.Currencies
            .Include(c => c.ExchangeRates)
            .ToListAsync();
    }

    public async Task AddAsync(Currency currency)
    {
        await _context.Currencies.AddAsync(currency);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Currency currency)
    {
        _context.Currencies.Update(currency);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveByCodeAsync(string code)
    {
        var currency = await GetWithExchangeRatesByCodeAsync(code);
        if (currency != null)
        {
            _context.Currencies.Remove(currency);
            await _context.SaveChangesAsync();
        }
    }
}
