using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application;

public static class DependencyInjection
{
    public static ServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register application services here

        return (ServiceCollection)services;
    }
}