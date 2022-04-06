using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
namespace MI.Cache
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InitRedisCache(this IServiceCollection services, IConfiguration configuration)
        {

            var redisConnection = configuration["Redis:ConnectionString"] + ",defaultDatabase=" + configuration["Redis:DefaultDatabase"];
            services.AddDistributedRedisCache(options => // config redis cache server
            {
                options.Configuration = redisConnection;
                options.InstanceName = configuration["Redis:InstanceName"];
            });

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));

            return services;
        }
    }
}
