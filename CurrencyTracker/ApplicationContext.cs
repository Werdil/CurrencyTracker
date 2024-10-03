using CurrencyTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyTracker;
public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        //this.Database.EnsureCreated();
    }

    public DbSet<Currency> Currencies { get; set; }
    public DbSet<ExchangeRate> ExchangeRates { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserCurrency> UsersCurrencies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<ExchangeRate>()
           .HasKey(e => e.Id);


        modelBuilder.Entity<User>(eb =>
        {
            eb.HasKey(c => c.Id);
            eb.HasMany(e => e.UserCurrencies)
            .WithMany(c => c.CurrencyUsers)
            .UsingEntity<UserCurrency>(u =>
            {
                u.HasOne(uc => uc.User)
                    .WithMany()
                    .HasForeignKey(uc => uc.UserId);

                u.HasOne(uc => uc.Currency)
                    .WithMany()
                    .HasForeignKey(uc => uc.CurrencyId);

                u.HasKey(x => new {x.UserId,x.CurrencyId });

                u.Property(x => x.CreatedDate)
                    .HasDefaultValueSql("GETDATE()");
            });
        });
    }
}