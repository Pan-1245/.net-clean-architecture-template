using CleanArchitecture.Master.Dto;
using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Courses.Queries.GetCourseQuery;

public class GetCourseQuery : IRequest<BaseResponse<CourseDto>>
{
    public Guid Id { get; set; }
}