namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Register API services here

        return services;
    }

    public static WebApplication UseApiConfiguration(this WebApplication app)
    {
        // Configure the HTTP request pipeline here

        return app;
    }
}