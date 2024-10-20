using CleanArchitecture.Master.Domain.Entities;
using CleanArchitecture.Interfaces.Mapper.Master;
using CleanArchitecture.Master.Dto;

namespace CleanArchitecture.Master.UseCases.Mapper;

public class CourseMapper : ICourseMapper<CourseDto>
{
    public static CourseDto MapCourseDto(Course course)
    {
        var response = new CourseDto
        {
            Id = course.Id,
            Name = course.Name,
            CreatedAt = course.CreatedAt,
            CreatedBy = course.CreatedBy,
            UpdatedAt = course.UpdatedAt,
            UpdatedBy = course.UpdatedBy,
        };

        return response;
    }
}