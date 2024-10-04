using CurrencyTracker.WebApi.Dtos;

namespace CurrencyTracker.Domain.Interfaces;
public interface IUserService
{
    Task RegisterAsync(RegisterDto dto);
    Task<string> LoginAsync(LoginDto dto);
    Task SubscribeCurrency(string userid, string code);
    Task<List<CurrencyRateInfoDto>> GetAllCurrencyRateInfosForUser(string userid);
    Task UnsubscribeCurrency(string userid, string code);
}
