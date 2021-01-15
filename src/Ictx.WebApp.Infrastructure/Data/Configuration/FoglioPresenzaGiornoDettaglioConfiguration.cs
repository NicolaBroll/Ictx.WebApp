using Ictx.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ictx.WebApp.Infrastructure.Data.Configuration
{
    public class FoglioPresenzaGiornoDettaglioConfiguration : IEntityTypeConfiguration<FoglioPresenzaGiornoDettaglio>
    {
        public void Configure(EntityTypeBuilder<FoglioPresenzaGiornoDettaglio> builder)
        {
            builder.ToTable("FoglioPresenzaGiornoDettaglio");

            builder.HasKey(ci => ci.Id);

            // Relazione FoglioPresenzaGiorno
            builder.HasOne(p => p.Giorno)
                .WithMany(b => b.Dettagli)
                .HasForeignKey(p => p.FoglioPresenzaGiornoId);

            // Relazione FoglioPresenzaVpa
            builder.HasOne(p => p.Vpa)
                .WithMany(b => b.Dettagli)
                .HasForeignKey(p => p.FoglioPresenzaVpaId);
        }
    }
}
