using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Occasia.Common.Application;

public static class DependencyInjection
{
    // Add Application
    public static IServiceCollection AddApplication(this IServiceCollection services, Assembly[] moduleAssemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);

#pragma warning disable S125
            // config.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            // config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
#pragma warning restore S125
        });

        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);

        return services;
    }
}
