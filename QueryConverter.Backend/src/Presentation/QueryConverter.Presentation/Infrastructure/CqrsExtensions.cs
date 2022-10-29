using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Shared.Cqrs.Dispatchers;
using QueryConverter.Shared.Cqrs.Queries;

namespace QueryConverter.Presentation.Infrastructure
{
    public static class CqrsExtensions
    {
        public static string projectName = "QueryConverter";

        public static IServiceCollection AddCqrs(this IServiceCollection services)
            => services
                     .AddHandlers()
                     .AddDispatchers();

        public static IServiceCollection AddDispatchers(this IServiceCollection services)
            => services
                     .AddSingleton<IDispatcher, Dispatcher>()
                     .AddSingleton<ICommandDispatcher, CommandDispatcher>()
                     .AddSingleton<IQueryDispatcher, QueryDispatcher>();

        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                                      .Where(x => x.FullName is not null && x.FullName.Contains(projectName))
                                      .ToArray();

            services.Scan(s => s.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            services.Scan(s => s.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }
    }
}
