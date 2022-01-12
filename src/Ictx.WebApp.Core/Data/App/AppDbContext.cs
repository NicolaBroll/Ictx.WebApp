using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ictx.WebApp.Core.Data.App.Configuration;
using Ictx.WebApp.Core.Domain.DipendenteDomain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ictx.WebApp.Core.Data.App;

public class AppDbContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;

    public DbSet<Dipendente> Dipendente => Set<Dipendente>();
       
    public AppDbContext(DbContextOptions<AppDbContext> options, ILoggerFactory loggerFactory) : base(options)
    {
        this._loggerFactory = loggerFactory;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new DipendenteConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLoggerFactory(this._loggerFactory);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");
    }
}

/// <summary>
/// Converts <see cref="DateOnly" /> to <see cref="DateTime"/> and vice versa.
/// </summary>
public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    /// <summary>
    /// Creates a new instance of this converter.
    /// </summary>
    public DateOnlyConverter() : base(
            d => d.ToDateTime(TimeOnly.MinValue),
            d => DateOnly.FromDateTime(d))
    { }
}

//public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
//{
//    public AppDbContext CreateDbContext(string[] args)
//    {
//        var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();

//        optionBuilder.UseSqlServer("Server=localhost;Database=WebApp_Development_2;User Id=sa;Password=Testing1122;");

//        return new AppDbContext(optionBuilder.Options);
//    }
//}

