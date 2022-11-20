using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmsSender.Common.RabbitMQ.Interfaces;
using SmsSender.Common.RabbitMQ.Options;

namespace SmsSender.Common.RabbitMQ;

/// <summary>
/// Конфигурация Rabbit для DI
/// </summary>
public static class RabbitConfiguration
{
    public static void ConfigureRabbit(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<RabbitConnectionOptions>( opt => 
            configuration.GetSection(nameof(RabbitConnectionOptions)).Bind(opt));

        services.Configure<RabbitQueueOptions>(opt =>
            configuration.GetSection(nameof(RabbitQueueOptions)).Bind(opt));

        services.AddScoped<IRabbitConnection, RabbitConnection>();
        services.AddScoped<IRabbitClient, RabbitClient>();
    }
}