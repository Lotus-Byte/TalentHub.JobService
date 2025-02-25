using JobBll.Contracts.Interface;
using JobBll.Service;
using JobBll.Settings;

using JobDataAccess.Infrastructure;

using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RabbitMQ.Client;

namespace JobBll.Infrastructure;

public static class JobBllRegisterExtensions
{
    public static IServiceCollection AddJobBllService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddJobDataAccessContext (configuration);

        services.AddMassTransit(x => {
            x.UsingRabbitMq((context, cfg) =>
            {
                ConfigureRmq(cfg, configuration);
            });
        });


        services
            .AddScoped<IJob, JobService>();

        return services;
    }
    /// <summary>
    /// Конфигурирование RMQ.
    /// </summary>
    /// <param name="configurator"> Конфигуратор RMQ. </param>
    /// <param name="configuration"> Конфигурация приложения. </param>
    private static void ConfigureRmq(IRabbitMqBusFactoryConfigurator configurator, IConfiguration configuration)
    {
        var rmqSettings = configuration.GetSection("RabbitMqSettings");
        var rmqOption = rmqSettings.Get<RabbitMqSettings>();
        configurator.Host(rmqOption.Host,
            rmqOption.VHost,
            h =>
            {
                h.Username(rmqOption.Login);
                h.Password(rmqOption.Password);
            });
    }
}
