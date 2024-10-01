using CurrencyTracker.Domain.Interfaces;
using CurrencyTracker.WebApi.Dtos;

namespace CurrencyTracker.Application.Services;
public class CurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICurrencyHelper _currencyHelper;


    public CurrencyService(ICurrencyRepository currencyRepository, ICurrencyHelper getCurrency)
    {
        _currencyRepository = currencyRepository;
        _currencyHelper = getCurrency;
    }

    public async Task<List<ExchangeRateDto>> GetCurrencyRates(string code)
    {
        var currency = await _currencyHelper.GetCurrency(code);
        return currency.ExchangeRates.Select(r => new ExchangeRateDto { Date = r.Date, Value = r.Value }).ToList();
    }



    public async Task<CurrencyRateInfoDto> GetCurrencyRateInfo(string code, int days)
    {
        var currency = await _currencyRepository.GetByCodeAsync(code);

        if (currency == null)
            return null;

        return new CurrencyRateInfoDto
        {
            Code = code,
            AverageRate = currency.GetAverageRate(days),
            MinRate = currency.GetMinRate(days),
            MaxRate = currency.GetMaxRate(days)
        };
    }
}

