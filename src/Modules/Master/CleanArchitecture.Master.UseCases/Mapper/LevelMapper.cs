using CleanArchitecture.Master.Domain.Entities;
using CleanArchitecture.Interfaces.Mapper.Master;
using CleanArchitecture.Utilities.Exceptions;
using CleanArchitecture.Master.Dto;

namespace CleanArchitecture.Master.UseCases.Mapper;

public class LevelMapper : ILevelMapper<LevelDto>
{
    public static LevelDto MapLevelDto(Level level, IEnumerable<Course> courses)
    {
        var course = courses.SingleOrDefault(l => l.Id == level.CourseId);

        if (course is null)
        {
            throw new GeneralException.NotFound($"Course with given ID ({level.CourseId}) is not found.");
        }

        var response = new LevelDto
        {
            Id = level.Id,
            Name = level.Name,
            CourseId = course.Id,
            CourseName = course.Name,
            IsActive = level.IsActive,
            CreatedAt = level.CreatedAt,
            CreatedBy = level.CreatedBy,
            UpdatedAt = level.UpdatedAt,
            UpdatedBy = level.UpdatedBy
        };

        return response;
    }
}