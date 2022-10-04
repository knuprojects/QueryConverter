namespace QueryConverter.Presentation.Infrastructure
{
    public static class AddDefaultServicesExtension
    {
        public static IServiceCollection AddDefaultServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
