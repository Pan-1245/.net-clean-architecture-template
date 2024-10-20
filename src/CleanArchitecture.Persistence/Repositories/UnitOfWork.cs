using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Interfaces.Persistence.Master;

namespace CleanArchitecture.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    #region Master Data Repository

    public ICourseRepository Courses { get; }
    public ISubjectRepository Subjects { get; }
    public ILevelRepository Levels { get; }

    #endregion

    public UnitOfWork(ICourseRepository courses,
                      ISubjectRepository subjects,
                      ILevelRepository levels)
    {
        Courses = courses ?? throw new ArgumentNullException(nameof(courses));
        Subjects = subjects ?? throw new ArgumentNullException(nameof(subjects));
        Levels = levels ?? throw new ArgumentNullException(nameof(levels));
    }

    public void Dispose()
    {
        System.GC.SuppressFinalize(this);
    }
}