using Ictx.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ictx.WebApp.Infrastructure.Data.BackgroundService
{
    public class BackgroundServiceDbContext : DbContext
    {
        public DbSet<Operation> Operation { get; set; }

        public BackgroundServiceDbContext(DbContextOptions<BackgroundServiceDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
