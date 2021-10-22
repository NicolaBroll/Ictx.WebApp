using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Entities.Base;
using Ictx.WebApp.Infrastructure.Data.App.Configuration;

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

            builder.ApplyConfiguration(new DipendenteConfiguration());
        }

        #region Save changes

        public override int SaveChanges()
        {
            SetInsertedAndUpdated();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetInsertedAndUpdated();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetInsertedAndUpdated();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SetInsertedAndUpdated();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SetInsertedAndUpdated()
        {
            var now = DateTime.Now;

            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).Updated = now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).Inserted = now;
                }
            }
        }

        #endregion
    }
}
