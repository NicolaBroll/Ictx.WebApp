using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data.App.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Ictx.WebApp.Infrastructure.Data.App
{
    public class AppDbContext : DbContextBase
    { 
        public DbSet<Dipendente> Dipendente { get; set; }
       
        public AppDbContext(DbContextOptions<DbContextBase> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new DipendenteConfiguration());
        }
    }
}
