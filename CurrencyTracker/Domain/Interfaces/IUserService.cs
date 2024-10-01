using CurrencyTracker.WebApi.Dtos;

namespace CurrencyTracker.Domain.Interfaces;
public interface IUserService
{
    Task RegisterAsync(string username, string password, string confirmPassword);
    Task<string> LoginAsync(string username, string password);
    Task SubscribeCurrency(string userid, string code);
    Task<List<CurrencyRateInfoDto>> GetAllCurrencyRateInfosForUser(string userid);
}
