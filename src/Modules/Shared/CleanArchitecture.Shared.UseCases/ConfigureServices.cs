using CleanArchitecture.Shared.UseCases.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Shared.UseCases;

public static class ConfigureServices
{
    public static void AddApplicationInjection(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
    }
}