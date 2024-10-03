using CurrencyTracker.Domain.Entities;
using CurrencyTracker.Domain.Interfaces;

namespace CurrencyTracker.Application.Services
{
    public class CurrencyHelperService : ICurrencyHelper
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly INbpApiClient _nbpApiClient;

        public CurrencyHelperService(INbpApiClient nbpApiClient, ICurrencyRepository currencyRepository)
        {
            _nbpApiClient = nbpApiClient;
            _currencyRepository = currencyRepository;
        }

        public async Task<Currency> DownloadAndSaveCurrency(string code)
        {
            var ratesFromApi = await _nbpApiClient.GetLastRates(code, 30);
            var currency = new Currency(code);
            foreach (var rate in ratesFromApi)
            {
                currency.AddExchangeRate(new ExchangeRate(rate.Date, rate.Value));
            }
            await _currencyRepository.AddAsync(currency);

            return currency;
        }
    }
}
