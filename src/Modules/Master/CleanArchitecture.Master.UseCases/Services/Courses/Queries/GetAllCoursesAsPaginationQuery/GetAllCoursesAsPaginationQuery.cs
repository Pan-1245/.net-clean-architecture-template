using CleanArchitecture.Master.Dto;
using CleanArchitecture.Master.UseCases.Common.Bases;
using CleanArchitecture.Shared.Domain.Enum;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Courses.Queries.GetAllCoursesAsPaginationQuery
{
    public class GetAllCoursesAsPaginationQuery : IRequest<BaseResponsePagination<IEnumerable<CourseDto>>>
    {
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public SortingOrder OrderBy { get; set; } = SortingOrder.ASC;
    }
}