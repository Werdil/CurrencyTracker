using CurrencyTracker.Domain.Entities;
using CurrencyTracker.Domain.Interfaces;

namespace CurrencyTracker.BackgroundServices;
public class ExchangeRateBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ExchangeRateBackgroundService> _logger;

    public ExchangeRateBackgroundService(IServiceProvider serviceProvider, ILogger<ExchangeRateBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("ExchangeRateBackgroundService is starting");

        await DoWork(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var now = DateTime.Now;
                var nextRun = new DateTime(now.Year, now.Month, now.Day, 2, 0, 0).AddDays(1);
                var delay = nextRun - now;

                await Task.Delay(delay, stoppingToken);
                await DoWork(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        _logger.LogInformation("ExchangeRateBackgroundService is stopping.");
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {

        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var nbpApiClient = scope.ServiceProvider.GetRequiredService<INbpApiClient>();

            var currencies = context.Currencies.ToList();
            foreach (var currency in currencies)
            {
                var yesterday = DateTime.Now.AddDays(-1);

                var rateDto = await nbpApiClient.GetRateForDate(currency.Code, yesterday);

                if (rateDto != null)
                {
                    var exchangeRate = new ExchangeRate(rateDto.Date, rateDto.Value);
                    currency.ExchangeRates.Add(exchangeRate);
                }
                try
                {
                    await context.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }

        }

    }
}

