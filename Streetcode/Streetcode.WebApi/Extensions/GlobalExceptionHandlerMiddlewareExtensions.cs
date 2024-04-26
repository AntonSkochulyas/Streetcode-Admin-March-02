using Streetcode.WebApi.ExceptionHandlers;

namespace Streetcode.WebApi.Extensions
{
    public static class GlobalExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }

        public static void AddGlobalExceptionHandlerMiddlewareToServices(this IServiceCollection services)
        {
            services.AddTransient<GlobalExceptionHandlerMiddleware>();
        }
    }
}
