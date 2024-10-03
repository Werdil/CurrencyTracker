using CurrencyTracker.WebApi.Dtos;

namespace CurrencyTracker.Domain.Interfaces;
public interface INbpApiClient
{
    Task<List<ExchangeRateDto>> GetLastRates(string code, int days);
    Task<ExchangeRateDto> GetRateForDate(string code, DateTime date);
}
