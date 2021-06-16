using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data.BackgroundService.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Ictx.WebApp.Infrastructure.Data.BackgroundService
{
    public class BackgroundServiceDbContext : DbContextBase
    {
        public DbSet<Operation> Operation { get; set; }

        public BackgroundServiceDbContext(DbContextOptions<DbContextBase> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new OperationConfiguration());
        }
    }
}
