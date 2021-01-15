﻿using Ictx.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ictx.WebApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Dipendente> Dipendente { get; set; }
        public DbSet<FoglioPresenza> FoglioPresenza { get; set; }
        public DbSet<FoglioPresenzaGiorno> FoglioPresenzaGiorno { get; set; }
        public DbSet<FoglioPresenzaGiornoDettaglio> FoglioPresenzaGiornoDettaglio { get; set; }
        public DbSet<FoglioPresenzaVpa> FoglioPresenzaVpa { get; set; }

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
