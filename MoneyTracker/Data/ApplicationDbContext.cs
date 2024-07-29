using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data.Configurations;
using MoneyTracker.Data.Models;
using MoneyTracker.Data.Seed;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Security.Claims;

namespace MoneyTracker.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : IdentityDbContext<User, Role, int>(options)
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<EntryType> EntryTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("main");

            builder
                .ApplyConfiguration(new CurrencyConfiguration())
                .ApplyConfiguration(new EntryConfiguration())
                .ApplyConfiguration(new EntryTypeConfiguration());

            builder
            .SeedCurrencyData()
            .SeedEntryTypeData();

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var softDeletes = ChangeTracker.Entries<ISoftDeletable>()
                .Where(e => e.State == EntityState.Deleted);

            foreach (var softDeleteItem in softDeletes)
            {
                softDeleteItem.Property(nameof(ISoftDeletable.DeletedAt)).CurrentValue = DateTime.UtcNow;
                softDeleteItem.Property(nameof(ISoftDeletable.DeletedBy)).CurrentValue = Convert.ToInt32(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
                softDeleteItem.State = EntityState.Modified;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
