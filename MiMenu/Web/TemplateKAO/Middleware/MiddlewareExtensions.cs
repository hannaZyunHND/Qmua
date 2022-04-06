using MI.Cache;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Threading.Tasks;
using TemplateKAO.ExecuteCommand;

namespace TemplateKAO.Middleware
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {

        }

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

        }

        public static IApplicationBuilder ClearCacheMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomClearCacheMiddleware>();
        }

    }


    public class CustomClearCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        //cache 
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _multiplexer;
        public CustomClearCacheMiddleware(RequestDelegate next, IConfiguration configuration, IDistributedCache distributedCache, IConnectionMultiplexer multiplexer)
        {
            _next = next;
            _configuration = configuration;
            _distributedCache = distributedCache;
            _multiplexer = multiplexer;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            RedisUtils.DeleteAllCacheAsynForce(_multiplexer, _configuration);
            await _next(httpContext);
        }
    }
}
