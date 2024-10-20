using CleanArchitecture.Master.Dto;
using CleanArchitecture.Master.UseCases.Common.Bases;
using CleanArchitecture.Shared.Domain.Enum;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Courses.Queries.GetAllCoursesQuery;

public class GetAllCoursesQuery : IRequest<BaseResponse<IEnumerable<CourseDto>>>
{
    public string? Name { get; set; }
    public bool? IsActive { get; set; }
    public string? SortBy { get; set; }
    public SortingOrder OrderBy { get; set; } = SortingOrder.ASC;
}