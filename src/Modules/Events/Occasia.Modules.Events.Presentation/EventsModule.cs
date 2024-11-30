using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Occasia.Modules.Events.Infrastructure;
using Occasia.Modules.Events.Presentation.Categories;

namespace Occasia.Modules.Events.Presentation;

public static class EventsModule
{
    public static IServiceCollection AddEventsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        return services;
    }


    public static IEndpointRouteBuilder MapEventModuleEndpoints(this IEndpointRouteBuilder app)
    {
        CategoryEndpoints.MapEndpoints(app);
        return app;
    }
}
