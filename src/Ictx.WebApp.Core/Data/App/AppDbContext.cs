using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ictx.WebApp.Core.Data.App.Configuration;
using Ictx.WebApp.Core.Domain.Dipendente;

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

