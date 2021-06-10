using Ictx.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ictx.WebApp.Infrastructure.Data.App
{
    public class AppDbContext : DbContext
    { 
        public DbSet<Dipendente> Dipendente { get; set; }
       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
