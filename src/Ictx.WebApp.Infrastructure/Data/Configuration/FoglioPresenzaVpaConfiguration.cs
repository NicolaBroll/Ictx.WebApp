using Ictx.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ictx.WebApp.Infrastructure.Data.Configuration
{
    public class FoglioPresenzaVpaConfiguration : IEntityTypeConfiguration<FoglioPresenzaVpa>
    {
        public void Configure(EntityTypeBuilder<FoglioPresenzaVpa> builder)
        {
            builder.ToTable("FoglioPresenzaVpa");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Sigla)
                .IsRequired(true)
                .HasColumnType("char(2)");

            builder.Property(ci => ci.Descrizione)
                .IsRequired(true)
                .HasMaxLength(256);

            // Relazione FoglioPresenza.
            builder.HasMany(p => p.Dettagli)
                .WithOne(b => b.Vpa)
                .HasForeignKey(p => p.FoglioPresenzaVpaId);
        }
    }
}
