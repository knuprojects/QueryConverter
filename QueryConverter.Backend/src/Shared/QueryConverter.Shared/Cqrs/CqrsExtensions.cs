using Microsoft.Extensions.DependencyInjection;
using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Shared.Cqrs.Dispatchers;
using QueryConverter.Shared.Cqrs.Queries;
using System.Reflection;

namespace QueryConverter.Shared.Cqrs
{
    public static class CqrsExtensions
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services, Assembly assemblies)
            => services
                     .AddDispatchers()
                     .AddHandlers(assemblies);

        public static IServiceCollection AddDispatchers(this IServiceCollection services)
            => services
                     .AddSingleton<IQueryDispatcher, QueryDispatcher>()
                     .AddSingleton<ICommandDispatcher, CommandDispatcher>()
                     .AddSingleton<IDispatcher, Dispatcher>();

        // TODO: fix scrutor logics
        public static IServiceCollection AddHandlers(this IServiceCollection services, Assembly assemblies)
        {
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
