using CurrencyTracker.Domain.Interfaces;
using CurrencyTracker.WebApi.Dtos;

namespace CurrencyTracker.Infrastructure.ApiClients;
public class NbpApiClient : INbpApiClient
{
    private readonly HttpClient _httpClient;

    public NbpApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ExchangeRateDto>> GetLastRates(string code,int days)
    {
        var response = await _httpClient.GetAsync($"https://api.nbp.pl/api/exchangerates/rates/a/{code}/last/{days}/?format=json");
        response.EnsureSuccessStatusCode();
        var rates = await response.Content.ReadFromJsonAsync<NbpApiResponse>();
        return rates.Rates.Select(r => new ExchangeRateDto { Date = r.EffectiveDate, Value = r.Mid }).ToList();
    }
}

public class NbpApiResponse
{
    public List<NbpRate> Rates { get; set; }
}

public class NbpRate
{
    public DateTime EffectiveDate { get; set; }
    public decimal Mid { get; set; }
}
