using Ictx.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ictx.WebApp.Infrastructure.Data.Configuration
{
    public class FoglioPresenzaGiornoConfiguration : IEntityTypeConfiguration<FoglioPresenzaGiorno>
    {
        public void Configure(EntityTypeBuilder<FoglioPresenzaGiorno> builder)
        {
            builder.ToTable("FoglioPresenzaGiorno");

            builder.HasKey(ci => ci.Id);

            // Relazione FoglioPresenza.
            builder.HasOne(p => p.FoglioPresenza)
                .WithMany(b => b.Giorni)
                .HasForeignKey(p => p.FoglioPresenzaId);

            // Relazione FoglioPresenzaGiornoDettaglio.
            builder.HasMany(p => p.Dettagli)
                .WithOne(b => b.Giorno)
                .HasForeignKey(p => p.FoglioPresenzaGiornoId);
        }
    }
}
