using CleanArchitecture.Master.Domain.Entities;

namespace CleanArchitecture.Interfaces.Mapper.Master;

public interface ILevelMapper<T> where T : class
{
    abstract static T MapLevelDto(Level level, IEnumerable<Course> courses);

}