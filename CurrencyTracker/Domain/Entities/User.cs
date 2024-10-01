using CurrencyTracker.WebApi.Dtos;

namespace CurrencyTracker.Domain.Entities;
public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string PasswordHash { get; private set; }
    public List<Currency> UserCurrencies { get; private set; }

    public User(string username, string passwordHash)
    {
        Id = Guid.NewGuid();
        Username = username;
        PasswordHash = passwordHash;
        UserCurrencies = new List<Currency>();
    }

    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }

    public async Task<List<CurrencyRateInfoDto>> GetCurrencyRateInfos(int days)
    {

        if (UserCurrencies == null)
            return null;

        return UserCurrencies.Select(c => c.GetCurrencyRateInfoDto(days)).ToList();
    }
}
