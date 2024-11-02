using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Behaviors;

namespace TaskManagement.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(AssemblyReference.Assembly);
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });
        return services;
    }
}
