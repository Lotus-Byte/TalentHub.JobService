using JobDataAccess.Context;
using JobDataAccess.Contracts.Repository;
using JobDataAccess.Internal;
using JobDataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobDataAccess.Infrastructure;

public static class JobDataAccessRegisterExtensions
{
    public static IServiceCollection AddJobDataAccessContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        var connectionStringSection = configuration.GetConnectionString(DbConfig.CONNECTION_STRING_KEY);
        services
            .AddDbContext<JobDbContext>(options =>
            {
                options
                    .UseNpgsql(connectionStringSection)
                    .UseSnakeCaseNamingConvention();
            });

        services
            .AddTransient<IJobRepository, JobRepository>();

        return services;
    }
}
