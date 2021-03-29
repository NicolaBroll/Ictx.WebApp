using Ictx.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ictx.WebApp.Infrastructure.Data.Configuration
{
    public class UfficioConfiguration : IEntityTypeConfiguration<Ufficio>
    {
        public void Configure(EntityTypeBuilder<Ufficio> builder)
        {
            builder.ToTable("Ufficio");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Denominazione)
                .IsRequired(true)
                .HasMaxLength(256);           

            // Relazione Ufficio.
            builder.HasOne(p => p.UfficioBase)
                .WithMany(b => b.LstUffici)
                .HasForeignKey(p => p.UfficioBaseId);

            // Relazione Impresa.
            builder.HasMany(p => p.LstImprese)
                .WithOne(b => b.Ufficio)
                .HasForeignKey(p => p.UfficioId);
        }
    }
}
