using Ictx.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ictx.WebApp.Infrastructure.Data.Configuration
{
    public class DittaConfiguration : IEntityTypeConfiguration<Ditta>
    {
        public void Configure(EntityTypeBuilder<Ditta> builder)
        {
            builder.ToTable("Ditta");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Denominazione)
                .IsRequired(true)
                .HasMaxLength(256);           

            // Relazione Impresa.
            builder.HasOne(p => p.Impresa)
                .WithMany(b => b.LstDitte)
                .HasForeignKey(p => p.ImpresaId);

            // Relazione Dipendente.
            builder.HasMany(p => p.LstDipendenti)
                .WithOne(b => b.Ditta)
                .HasForeignKey(p => p.DittaId);
        }
    }
}
