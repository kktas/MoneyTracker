using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data.Models;

namespace MoneyTracker.Data.Seed
{
    public static partial class Seeder
    {
        internal static ModelBuilder SeedCurrencyData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
                new Currency() { Id = 1, Name = "TL", Symbol = "₺" },
                new Currency() { Id = 2, Name = "Dolar", Symbol = "$" },
                new Currency() { Id = 3, Name = "Euro", Symbol = "€" }
            );

            return modelBuilder;
        }
    }
}
