using Microsoft.AspNetCore.Builder;

namespace JanHome.CMS.Middleware
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
    }
}
