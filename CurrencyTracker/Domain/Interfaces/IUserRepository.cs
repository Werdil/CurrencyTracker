using CurrencyTracker.Domain.Entities;

namespace CurrencyTracker.Domain.Interfaces;
public interface IUserRepository
{
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByUserIdAsync(Guid id);
    Task<User> GetByUserIdWithCurrenciesAsync(Guid id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}
