using CurrencyTracker.Domain.Entities;
using CurrencyTracker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CurrencyTracker.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
    public async Task<User> GetByUserIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
    public async Task<User> GetByUserIdWithCurrenciesAsync(Guid id, Expression<Func<Currency, object>> includeExpression)
    {
        return await _context.Users
            .Include(u => u.UserCurrencies)
            .ThenInclude(includeExpression)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> GetByUserIdWithCurrenciesAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.UserCurrencies)
            .ThenInclude(u => u.ExchangeRates)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteUserCurrencyAsync(Guid userId, Guid currencyId)
    {

        var usersCurrency = await _context.UsersCurrencies
            .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CurrencyId == currencyId);

        if (usersCurrency != null)
        {
            _context.UsersCurrencies.Remove(usersCurrency);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsAnyUserSubscribedToCurrencyAsync(Guid currencyId)
    {
        return await _context.UsersCurrencies
            .AnyAsync(uc => uc.CurrencyId == currencyId);
    }

}
