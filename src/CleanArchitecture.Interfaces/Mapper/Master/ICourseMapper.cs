using CleanArchitecture.Master.Domain.Entities;

namespace CleanArchitecture.Interfaces.Mapper.Master;

public interface ICourseMapper<T> where T : class
{
    abstract static T MapCourseDto(Course course);
}