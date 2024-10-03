using AutoMapper;
using CurrencyTracker.Domain.Interfaces;
using CurrencyTracker.WebApi.Dtos;

namespace CurrencyTracker.Application.Services;
public class CurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICurrencyHelper _currencyHelper;
    private readonly IMapper _mapper;


    public CurrencyService(ICurrencyRepository currencyRepository, ICurrencyHelper getCurrency, IMapper mapper)
    {
        _currencyRepository = currencyRepository;
        _currencyHelper = getCurrency;
        _mapper = mapper;
    }

    public async Task<List<ExchangeRateDto>> GetCurrencyRates(string code)
    {
        var currency = await _currencyRepository.GetWithExchangeRatesByCodeAsync(code);
        if (currency == null)
        {
            currency = await _currencyHelper.DownloadAndSaveCurrency(code);
        }
        return currency.ExchangeRates.Select(r => new ExchangeRateDto { Date = r.Date, Value = r.Value }).ToList();
    }

    public async Task<decimal> CalculateEMA(string code, DateTime date, int days)
    {
        var currency = await _currencyRepository.GetWithExchangeRatesByCodeAsync(code);
        var ema = currency.CalculateEMA(date, days);
        return ema;
    }

    public async Task<CurrencyRateInfoDto> GetCurrencyRateInfo(string code, int days)
    {
        var currency = await _currencyRepository.GetWithExchangeRatesByCodeAsync(code);

        if (currency == null)
            return null;

        var dto = _mapper.Map<CurrencyRateInfoDto>(currency);
        return dto;
    }
}

