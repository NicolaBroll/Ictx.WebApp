using Ictx.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ictx.WebApp.Infrastructure.Data.Configuration
{
    public class UfficioBaseConfiguration : IEntityTypeConfiguration<UfficioBase>
    {
        public void Configure(EntityTypeBuilder<UfficioBase> builder)
        {
            builder.ToTable("UfficioBase");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Denominazione)
                .IsRequired(true)
                .HasMaxLength(256);

            // Relazione Ditta.
            builder.HasMany(p => p.LstUffici)
                .WithOne(b => b.UfficioBase)
                .HasForeignKey(p => p.UfficioBaseId);
        }
    }
}
