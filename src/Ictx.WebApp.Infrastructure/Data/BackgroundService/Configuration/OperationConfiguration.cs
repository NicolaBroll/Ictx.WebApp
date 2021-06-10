using Ictx.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ictx.WebApp.Infrastructure.Data.BackgroundService.Configuration
{
    public class OperationConfiguration : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {
            builder.ToTable(nameof(Operation));

            builder.HasKey(ci => ci.Id);

            //builder.Property(ci => ci.CodiceFiscale)
            //    .IsRequired(true)
            //    .HasColumnType("char(16)");

            //builder.Property(ci => ci.Nome)
            //    .IsRequired(true)
            //    .HasMaxLength(64);

            //builder.Property(ci => ci.Cognome)
            //    .IsRequired(true)
            //    .HasMaxLength(64);

            //builder.Property(ci => ci.Sesso)
            //    .IsRequired(true)
            //    .HasColumnType("char(1)");

            //builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
