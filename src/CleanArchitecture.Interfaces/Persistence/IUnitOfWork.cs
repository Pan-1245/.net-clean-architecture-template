using CleanArchitecture.Interfaces.Persistence.Master;

namespace CleanArchitecture.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    ICourseRepository Courses { get; }
    ISubjectRepository Subjects { get; }
    ILevelRepository Levels { get; }
}