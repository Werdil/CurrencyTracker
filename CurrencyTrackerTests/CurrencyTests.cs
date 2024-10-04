using CurrencyTracker.Domain.Entities;

namespace CurrencyTrackerTests;
public class CurrencyTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var code = "USD";

        // Act
        var currency = new Currency(code);

        // Assert
        Assert.Equal(code.ToUpper(), currency.Code);
        Assert.NotEqual(Guid.Empty, currency.Id);
        Assert.Empty(currency.ExchangeRates);
    }

    [Fact]
    public void AddExchangeRate_ShouldAddRateToList()
    {
        // Arrange
        var currency = new Currency("USD");
        var rate = new ExchangeRate(DateTime.Today, 1.2m);

        // Act
        currency.AddExchangeRate(rate);

        // Assert
        Assert.Single(currency.ExchangeRates);
        Assert.Contains(rate, currency.ExchangeRates);
    }

    [Fact]
    public void GetAverageRate_ShouldReturnCorrectAverage()
    {
        // Arrange
        var currency = new Currency("USD");
        currency.AddExchangeRate(new ExchangeRate(DateTime.Today, 1.2m));
        currency.AddExchangeRate(new ExchangeRate(DateTime.Today, 1.3m));

        // Act
        var averageRate = currency.GetAverageRate();

        // Assert
        Assert.Equal(1.25m, averageRate);
    }

    [Fact]
    public void GetRateForYesterday_ShouldReturnCorrectRate()
    {
        // Arrange
        var currency = new Currency("USD");
        var yesterday = DateTime.Today.AddDays(-1);
        var rate = new ExchangeRate(yesterday, 1.2m);
        currency.AddExchangeRate(rate);

        // Act
        var rateForYesterday = currency.GetRateForYesterday();

        // Assert
        Assert.Equal(1.2m, rateForYesterday);
    }

    [Fact]
    public void GetMinRate_ShouldReturnCorrectMinRate()
    {
        // Arrange
        var currency = new Currency("USD");
        currency.AddExchangeRate(new ExchangeRate(DateTime.Today, 1.2m));
        currency.AddExchangeRate(new ExchangeRate(DateTime.Today, 1.1m));
        currency.AddExchangeRate(new ExchangeRate(DateTime.Today, 1.3m));

        // Act
        var minRate = currency.GetMinRate();

        // Assert
        Assert.Equal(1.1m, minRate);
    }

    [Fact]
    public void GetMaxRate_ShouldReturnCorrectMaxRate()
    {
        // Arrange
        var currency = new Currency("USD");
        currency.AddExchangeRate(new ExchangeRate(DateTime.Today, 1.2m));
        currency.AddExchangeRate(new ExchangeRate(DateTime.Today, 1.1m));
        currency.AddExchangeRate(new ExchangeRate(DateTime.Today, 1.3m));

        // Act
        var maxRate = currency.GetMaxRate();

        // Assert
        Assert.Equal(1.3m, maxRate);
    }
}