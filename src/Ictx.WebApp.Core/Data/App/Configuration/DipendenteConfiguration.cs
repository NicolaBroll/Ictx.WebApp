using Ictx.WebApp.Core.Domain.Dipendente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ictx.WebApp.Core.Data.App.Configuration
{
    public class DipendenteConfiguration : IEntityTypeConfiguration<Dipendente>
    {
        public void Configure(EntityTypeBuilder<Dipendente> builder)
        {
            builder.ToTable(nameof(Dipendente));

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.IdDitta)
                .IsRequired(true);

            builder.Property(ci => ci.Nome)
                .IsRequired(true)
                .HasMaxLength(64);

            builder.Property(ci => ci.Cognome)
                .IsRequired(true)
                .HasMaxLength(64);

            builder.Property(ci => ci.Sesso)
                .IsRequired(true)
                .HasConversion<string>()
                .HasColumnType("char(1)");

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
