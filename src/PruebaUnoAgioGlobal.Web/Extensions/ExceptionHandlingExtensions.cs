using PruebaUnoAgioGlobal.Web.Exceptions.Handlers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PruebaUnoAgioGlobal.Web.Extensions
{
    public static class ExceptionHandlingExtensions
    {
        public static void AddExceptionHandler<THandler>(this IServiceCollection services) where THandler : class, IExceptionHandler
        {
            services.AddSingleton<IExceptionHandler, THandler>();
        }

        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        {
            var exceptionHandler = builder.ApplicationServices.GetService<IExceptionHandler>();
            return builder.UseExceptionHandler(applicationBuilder =>
            {
                applicationBuilder.Run(
                    async context =>
                    {
                        await exceptionHandler.Handle(context);
                    });
            });
        }
    }
}
