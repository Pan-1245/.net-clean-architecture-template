using CleanArchitecture.Master.Domain.Entities;

namespace CleanArchitecture.Interfaces.Mapper.Master;

public interface ISubjectMapper<T> where T : class
{
    abstract static T MapSubjectDto(Subject subject, IEnumerable<Course> courses);
};