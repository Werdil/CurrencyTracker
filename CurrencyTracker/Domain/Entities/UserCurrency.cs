namespace CurrencyTracker.Domain.Entities;

public class UserCurrency
{
    public Guid UserId { get; private set; }
    public Guid CurrencyId { get; private set; }
    public DateTime CreatedDate { get; private set; }

    public User User { get; private set; }
    public Currency Currency { get; private set; }
}

