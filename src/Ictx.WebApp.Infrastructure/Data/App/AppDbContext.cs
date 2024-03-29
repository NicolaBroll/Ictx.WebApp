﻿using Microsoft.EntityFrameworkCore;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data.App.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace Ictx.WebApp.Infrastructure.Data.App;

public class AppDbContext : DbContext
{
    public DbSet<Dipendente> Dipendente => Set<Dipendente>();
       
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new DipendenteConfiguration());
    }   
}

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionBuilder.UseSqlServer("Server=localhost;Database=WebApp_Development_2;User Id=sa;Password=Testing1122;");

        return new AppDbContext(optionBuilder.Options);
    }
}

