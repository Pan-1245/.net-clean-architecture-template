using CleanArchitecture.Master.Domain.Entities;
using CleanArchitecture.Interfaces.Mapper.Master;
using CleanArchitecture.Utilities.Exceptions;
using CleanArchitecture.Master.Dto;

namespace CleanArchitecture.Interfaces.Mapper;

public class SubjectMapper : ISubjectMapper<SubjectDto>
{
    public static SubjectDto MapSubjectDto(Subject subject, IEnumerable<Course> courses)
    {
        var course = courses.SingleOrDefault(c => c.Id == subject.Id);

        if (course is null)
        {
            throw new GeneralException.NotFound($"Course with given ID ({subject.CourseId}) is not found.");
        }

        var response = new SubjectDto
        {
            Id = subject.Id,
            Name = subject.Name,
            CourseId = course.Id,
            CourseName = course.Name,
            CreatedAt = subject.CreatedAt,
            CreatedBy = subject.CreatedBy,
            UpdatedAt = subject.UpdatedAt,
            UpdatedBy = subject.UpdatedBy
        };

        return response;
    }
}