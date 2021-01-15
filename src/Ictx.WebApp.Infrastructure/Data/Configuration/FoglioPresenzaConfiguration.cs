using Ictx.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ictx.WebApp.Infrastructure.Data.Configuration
{
    public class FoglioPresenzaConfiguration : IEntityTypeConfiguration<FoglioPresenza>
    {
        public void Configure(EntityTypeBuilder<FoglioPresenza> builder)
        {
            builder.ToTable("FoglioPresenza");

            builder.HasKey(ci => ci.Id);

            // Relazione dipendente.
            builder.HasOne(p => p.Dipendente)
                .WithMany(b => b.LstFoglioPresenza)
                .HasForeignKey(p => p.DipendenteId);

            // Relazione giorno.
            builder.HasMany(p => p.Giorni)
                .WithOne(b => b.FoglioPresenza)
                .HasForeignKey(p => p.FoglioPresenzaId);
        }
    }
}
