using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SmsSender.Common.Redis;

/// <summary>
/// Redis DI
/// </summary>
public static class RedisConfiguration
{
    public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RedisOptions>(opt =>
            configuration.GetSection(nameof(RedisOptions)).Bind(opt));
    }
}
