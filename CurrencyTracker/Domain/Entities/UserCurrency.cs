namespace CurrencyTracker.Domain.Entities;

public class UserCurrency
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CurrencyId { get; set; }
    public DateTime CreatedDate { get; set; }

    public User User { get; set; }
    public Currency Currency { get; set; }
}

