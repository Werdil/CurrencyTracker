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

                u.HasKey(x => x.Id);
                u.Property(x => x.Id)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("NEWID()");
                u.Property(x => x.CreatedDate)
                    .HasDefaultValueSql("GETDATE()");
            });
        });
    }
}