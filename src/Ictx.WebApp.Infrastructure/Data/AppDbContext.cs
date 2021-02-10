using Ictx.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ictx.WebApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UfficioBase> UfficioBase { get; set; }
        public DbSet<Ufficio> Ufficio { get; set; }
        public DbSet<Impresa> Impresa { get; set; }
        public DbSet<Ditta> Ditta { get; set; }
        public DbSet<Dipendente> Dipendente { get; set; }
       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
