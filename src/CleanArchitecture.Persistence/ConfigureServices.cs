using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Interfaces.Persistence.Master;
using CleanArchitecture.Persistence.Repositories;
using CleanArchitecture.Persistence.Repositories.Master;

using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistenceInjection(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        #region Master Data Repository Injection

        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<ILevelRepository, LevelRepository>();

        #endregion

        return services;
    }
}