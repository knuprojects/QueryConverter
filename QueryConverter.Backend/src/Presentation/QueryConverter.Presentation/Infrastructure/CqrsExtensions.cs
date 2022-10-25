using QueryConverter.Shared.Cqrs.Queries;

namespace QueryConverter.Presentation.Infrastructure
{
    public static class CqrsExtensions
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
            => services
                     .AddQueries();

        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }
    }
}
