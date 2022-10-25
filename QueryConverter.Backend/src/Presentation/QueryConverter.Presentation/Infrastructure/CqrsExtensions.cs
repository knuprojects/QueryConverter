using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Shared.Cqrs.Dispatchers;
using QueryConverter.Shared.Cqrs.Queries;

namespace QueryConverter.Presentation.Infrastructure
{
    public static class CqrsExtensions
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
            => services
                     .AddDispatchers()
                     .AddCommands()
                     .AddQueries();

        public static IServiceCollection AddDispatchers(this IServiceCollection services)
            => services
                     .AddSingleton<IDispatcher, Dispatcher>();

        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }
    }
}
