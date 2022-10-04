using QueryConverter.Core.Handlers;
using QueryConverter.Core.Processor;
using QueryConverter.Presentation.Middlewares.Infrastructure;

namespace QueryConverter.Presentation.Infrastructure
{
    public static class AddCoreExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<ExceptionMiddleware>();
            services.AddScoped<IQueryHandler, QueryHandler>();
            services.AddScoped<IQueryProcessor, QueryProcessor>();

            return services;
        }
    }
}
