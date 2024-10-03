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
        var nbpResponse = await response.Content.ReadFromJsonAsync<NbpApiResponse>();

        if (nbpResponse != null && nbpResponse.Rates.Any())
        {
            return nbpResponse.Rates.Select(r => new ExchangeRateDto { Date = r.EffectiveDate, Value = r.Mid }).ToList();
        };
        return null;
    }

    public async Task<ExchangeRateDto> GetRateForDate(string code, DateTime date)
    {
        var formattedDate = date.ToString("yyyy-MM-dd");
        var response = await _httpClient.GetAsync($"https://api.nbp.pl/api/exchangerates/rates/a/{code}/{formattedDate}");

        if (response.IsSuccessStatusCode)
        {
            var nbpResponse = await response.Content.ReadFromJsonAsync<NbpApiResponse>();

            if (nbpResponse.Rates != null && nbpResponse.Rates.Any())
            {
                return new ExchangeRateDto
                {
                    Date = nbpResponse.Rates.First().EffectiveDate,
                    Value = nbpResponse.Rates.First().Mid
                };
            }
        }
        return null;
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
