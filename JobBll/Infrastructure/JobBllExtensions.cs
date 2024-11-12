using JobBll.Contracts.Interface;
using JobBll.Service;
using JobDataAccess.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobBll.Infrastructure;

public static class JobBllRegisterExtensions
{
    public static IServiceCollection AddJobBllService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddJobDataAccessContext (configuration);

        services
            .AddScoped<IJob, JobService>();

        return services;
    }
}
