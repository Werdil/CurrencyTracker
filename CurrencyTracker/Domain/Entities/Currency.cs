using CurrencyTracker.WebApi.Dtos;

namespace CurrencyTracker.Domain.Entities;
public class Currency
{
    private string _code;
    public Guid Id { get; set; }
    public string Code
    {
        get => _code;
        set => _code = value?.ToUpper();
    }
    public List<ExchangeRate> ExchangeRates { get; set; }
    public List<User> CurrencyUsers { get; set; }

    public Currency(string code)
    {
        Id = Guid.NewGuid();
        Code = code;
        ExchangeRates = new List<ExchangeRate>();
    }

    public void AddExchangeRate(ExchangeRate rate)
    {
        ExchangeRates.Add(rate);
    }

    public List<ExchangeRate> GetLastDaysRates(int days)
    {
        return ExchangeRates.OrderByDescending(r => r.Date)
                            .Take(days)
                            .ToList();
    }

    public decimal? GetAverageRate(int days)
    {
        var rates = GetLastDaysRates(days);
        return rates.Any() ? rates.Average(r => r.Value) : null;
    }

    public decimal? GetMinRate(int days)
    {
        var rates = GetLastDaysRates(days);
        return rates.Any() ? rates.Min(r => r.Value) : null;
    }

    public decimal? GetMaxRate(int days)
    {
        var rates = GetLastDaysRates(days);
        return rates.Any() ? rates.Max(r => r.Value) : null;
    }

    public CurrencyRateInfoDto GetCurrencyRateInfoDto(int days)
    {
        return new CurrencyRateInfoDto
        {
            Code = Code,
            AverageRate = GetAverageRate(days),
            MinRate = GetMinRate(days),
            MaxRate = GetMaxRate(days)
        };
    }
}
