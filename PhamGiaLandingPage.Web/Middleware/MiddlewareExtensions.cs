using Mi.MemoryCache;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using PhamGiaLandingPage.Web.ExecuteCommand;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Middleware
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

        private ICacheController _memory;
        public CustomClearCacheMiddleware(RequestDelegate next, IConfiguration configuration, ICacheController memory)
        {
            _next = next;
            _configuration = configuration;
            _memory = memory;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            _memory.RemoveAll();
            await _next(httpContext);
        }
    }
}
