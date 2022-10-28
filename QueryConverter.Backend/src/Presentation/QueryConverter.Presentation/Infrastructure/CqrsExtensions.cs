using QueryConverter.Core.Handlers;
using QueryConverter.Core.Handlers.Commands;
using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Shared.Cqrs.Dispatchers;
using QueryConverter.Shared.Cqrs.Queries;
using QueryConverter.Types.Shared.Dto;

namespace QueryConverter.Presentation.Infrastructure
{
    public static class CqrsExtensions
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
            => services
                     .AddCommands()
                     .AddQueries()
                     .AddDispatchers();

        public static IServiceCollection AddDispatchers(this IServiceCollection services)
            => services
                     .AddSingleton<IDispatcher, Dispatcher>()
                     .AddSingleton<ICommandDispatcher, CommandDispatcher>()
                     .AddSingleton<IQueryDispatcher, QueryDispatcher>();

        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }
    }
}
