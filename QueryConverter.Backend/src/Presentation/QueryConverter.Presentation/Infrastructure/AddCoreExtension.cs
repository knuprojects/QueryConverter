using QueryConverter.Core.Utils;
using QueryConverter.Core.Utils.Factories;
using QueryConverter.Presentation.Middlewares.Infrastructure;

namespace QueryConverter.Presentation.Infrastructure
{
    public static class AddCoreExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
            => services
                    .AddSingleton<ExceptionMiddleware>()
                    .AddSingleton<ICondition, Condition>()
                    .AddSingleton<IStatementGeneratorFactory, SelectStatementGenerator>()
                    .AddSingleton<IStatementGeneratorFactory, OperationByStatementGenerator>()
                    .AddSingleton<StatementFactory>();

        public static IApplicationBuilder UseCore(this IApplicationBuilder builder)
            => builder.UseMiddleware<ExceptionMiddleware>();
    }
}
