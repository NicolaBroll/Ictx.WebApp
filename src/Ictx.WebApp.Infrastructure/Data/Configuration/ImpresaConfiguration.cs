using Ictx.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ictx.WebApp.Infrastructure.Data.Configuration
{
    public class ImpresaConfiguration : IEntityTypeConfiguration<Impresa>
    {
        public void Configure(EntityTypeBuilder<Impresa> builder)
        {
            builder.ToTable("Impresa");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Denominazione)
                .IsRequired(true)
                .HasMaxLength(256);           

            // Relazione Ufficio.
            builder.HasOne(p => p.Ufficio)
                .WithMany(b => b.LstImprese)
                .HasForeignKey(p => p.UfficioId);

            // Relazione Ditta.
            builder.HasMany(p => p.LstDitte)
                .WithOne(b => b.Impresa)
                .HasForeignKey(p => p.ImpresaId);
        }
    }
}
