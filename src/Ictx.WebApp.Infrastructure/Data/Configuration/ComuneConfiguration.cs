using Ictx.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ictx.WebApp.Infrastructure.Data.Configuration
{
    public class ComuneConfiguration : IEntityTypeConfiguration<Comune>
    {
        public void Configure(EntityTypeBuilder<Comune> builder)
        {
            builder.ToTable("Comune");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Codice)
                .IsRequired(true)
                .HasColumnType("char(4)");

            builder.Property(ci => ci.Provincia)
                .IsRequired(true)
                .HasColumnType("char(2)");

            builder.Property(ci => ci.Denominazione)
                .IsRequired(true)
                .HasMaxLength(256);
        }
    }
}
