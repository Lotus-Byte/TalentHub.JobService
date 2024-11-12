using JobDataAccess.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JobDataAccess.Context;

public class JobDbContext : DbContext
{
    public virtual DbSet<Entities.Job> Books { get; set; }

    public JobDbContext(DbContextOptions<JobDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString(DbConfig.CONNECTION_STRING_KEY);

            optionsBuilder
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        }
    }

    #region Required
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DbConfig.SCHEMA_NAME);

        modelBuilder.UseIdentityByDefaultColumns();
        
        modelBuilder.HasPostgresExtension(DbConfig.SCHEMA_NAME, "uuid-ossp");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Fluents.Job).Assembly);
    }
    #endregion
}
