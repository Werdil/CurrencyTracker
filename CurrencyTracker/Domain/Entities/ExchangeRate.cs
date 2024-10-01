namespace CurrencyTracker.Domain.Entities;
public class ExchangeRate
{
    public Guid Id { get; private set; }
    public DateTime Date { get; private set; }
    public decimal Value { get; private set; }

    public ExchangeRate(DateTime date, decimal value)
    {
        Date = date;
        Value = value;
    }
}
