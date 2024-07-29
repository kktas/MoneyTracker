using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyTracker.Data.Models;

namespace MoneyTracker.Data.Configurations
{
    public class EntryConfiguration : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Amount)
                .IsRequired();

            builder.Property(e => e.Description).IsRequired();

            builder.Property(e => e.At).IsRequired();

            builder.Property(e => e.CreatedAt).IsRequired();

            builder.HasOne(e => e.EntryType)
             .WithMany(et => et.Entries)
             .HasForeignKey(e => e.EntryTypeId);

            builder.HasOne(e => e.Currency)
                .WithMany(c => c.Entries)
                .HasForeignKey(e => e.CurrencyId);

            builder.HasQueryFilter(e => EF.Property<DateTime?>(e, "DeletedAt") == null);
        }
    }
}
