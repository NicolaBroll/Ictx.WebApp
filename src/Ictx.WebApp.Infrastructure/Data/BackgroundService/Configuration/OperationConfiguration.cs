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

            builder.Property(ci => ci.Tipo)
                .IsRequired(true)
                .HasColumnType("varchar(32)");

            builder.Property(ci => ci.Data)
                .IsRequired(true)
                .HasColumnType("nvarchar(max)");
        }
    }
}
