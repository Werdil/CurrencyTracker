using CurrencyTracker.Domain.Entities;
using System.Linq.Expressions;

namespace CurrencyTracker.Domain.Interfaces;
public interface IUserRepository
{
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByUserIdAsync(Guid id);
    Task<User> GetByUserIdWithCurrenciesAsync(Guid id);
    Task<User> GetByUserIdWithCurrenciesAsync(Guid id, Expression<Func<Currency, object>> expression);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteUserCurrencyAsync(Guid userId, Guid currencyId);
}
