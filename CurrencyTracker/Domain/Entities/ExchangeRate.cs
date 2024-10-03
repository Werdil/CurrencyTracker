namespace CurrencyTracker.Domain.Entities;
public class ExchangeRate
{
    public DateTime Date { get; private set; }
    public decimal Value { get; private set; }
    public Guid CurrencyId { get; private set; }

    public ExchangeRate(DateTime date, decimal value)
    {
        Date = date;
        Value = value;
    }
}
