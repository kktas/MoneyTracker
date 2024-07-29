using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyTracker.Data.Models;

namespace MoneyTracker.Data.Configurations
{
    public class EntryTypeConfiguration : IEntityTypeConfiguration<EntryType>
    {
        public void Configure(EntityTypeBuilder<EntryType> builder)
        {
            builder.HasKey(et => et.Id);

            builder.Property(et => et.Name)
                .IsRequired();
        }
    }
}
