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
        public static string projectName = "QueryConverter";

        public static IServiceCollection AddCqrs(this IServiceCollection services)
            => services
                     //.AddHandlers()
                     .AddDispatchers();

        public static IServiceCollection AddDispatchers(this IServiceCollection services)
            => services
                     .AddSingleton<IDispatcher, Dispatcher>()
                     .AddSingleton<IQueryDispatcher, QueryDispatcher>()
                     .AddSingleton<ICommandDispatcher, CommandDispatcher>();

        // TODO: fix scrutor logics
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<ICommand<ResultModel>, SelectCommand>();
            services.AddScoped<ICommandHandler<SelectCommand, ResultModel>, SelectCommandHandler>();
            services.AddScoped<ICommand<ResultModel>, OrderByCommand>();
            services.AddScoped<ICommandHandler<OrderByCommand, ResultModel>, OrderByCommandHandler>();
            services.AddScoped<ICommand<ResultModel>, GroupByCommand>();
            services.AddScoped<ICommandHandler<GroupByCommand, ResultModel>, GroupByCommandHandler>();

            //var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            //                          .Where(x => x.FullName is not null && x.FullName.Contains(projectName))
            //                          .ToArray();

            //services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            //           .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            //           .AsImplementedInterfaces()
            //           .WithScopedLifetime());

            //services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            //           .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
            //           .AsImplementedInterfaces()
            //           .WithScopedLifetime());

                return services;
            //}
        }
    }
}
