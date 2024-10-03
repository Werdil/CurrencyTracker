using CurrencyTracker.WebApi.Dtos;

namespace CurrencyTracker.Domain.Entities;
public class Currency
{
    private string _code;
    public Guid Id { get; private set; }
    public string Code
    {
        get => _code;
        private set => _code = value?.ToUpper();
    }
    public List<ExchangeRate> ExchangeRates { get; private set; }
    public List<User> CurrencyUsers { get; private set; }

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


    public decimal? GetAverageRate()
    {
        return ExchangeRates.Any() ? ExchangeRates.Average(r => r.Value) : null;
    }

    public decimal? GetMinRate()
    {
        return ExchangeRates.Any() ? ExchangeRates.Min(r => r.Value) : null;
    }

    public decimal? GetMaxRate()
    {
        return ExchangeRates.Any() ? ExchangeRates.Max(r => r.Value) : null;
    }
    public decimal? GetRateForYesterday()
    {
        var yesterday = DateTime.Today.AddDays(-1);
        var rate = ExchangeRates
            .FirstOrDefault(r => r.Date.Date == yesterday.Date);

        return rate?.Value; 
    }

    public decimal CalculateEMA(DateTime date, int days)
    {
        var ratesUntilDate = ExchangeRates
            .Where(r => r.Date <= date)
            .OrderBy(r => r.Date)
            .ToList();

        if (ratesUntilDate.Count < days)
        {
            throw new InvalidOperationException("There is not enough data to calculate EMA");
        }

        decimal smoothingFactor = 2m / (days + 1);
        decimal sma = ratesUntilDate.Take(days).Average(r => r.Value);
        decimal ema = sma;

        foreach (var rate in ratesUntilDate.Skip(days)) 
        {
            ema = (rate.Value - ema) * smoothingFactor + ema;
        }
        return ema;
    }
}
