using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data.Models;

namespace MoneyTracker.Data.Seed
{
    public static partial class Seeder
    {
        internal static ModelBuilder SeedEntryTypeData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntryType>().HasData(
                new EntryType() { Id = 1, Name = "Gider" },
                new EntryType() { Id = 2, Name = "Gelir" }
            );

            return modelBuilder;
        }
    }
}
