using QueryConverter.Core.Utils;
using QueryConverter.Core.Utils.Strategies;
using QueryConverter.Presentation.Middlewares.Infrastructure;

namespace QueryConverter.Presentation.Infrastructure
{
    public static class AddCoreExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
            => services
                    .AddSingleton<ExceptionMiddleware>()
                    .AddSingleton<ICondition, Condition>()
                    .AddScoped<IStatementGeneratorStrategy, SelectStatementGenerator>()
                    .AddScoped<IStatementGeneratorStrategy, OperationByStatementGenerator>()
                    .AddScoped<StatementStrategy>();

        public static IApplicationBuilder UseCore(this IApplicationBuilder builder)
            => builder.UseMiddleware<ExceptionMiddleware>();
    }
}
