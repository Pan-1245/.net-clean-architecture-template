using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.Dto;
using CleanArchitecture.Master.Dto.SearchFilter;
using CleanArchitecture.Master.UseCases.Common.Bases;
using CleanArchitecture.Master.UseCases.Mapper;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Courses.Queries.GetAllCoursesAsPaginationQuery;

public class GetAllCoursesAsPaginationQueryHandler : IRequestHandler<GetAllCoursesAsPaginationQuery, BaseResponsePagination<IEnumerable<CourseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCoursesAsPaginationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponsePagination<IEnumerable<CourseDto>>> Handle(GetAllCoursesAsPaginationQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponsePagination<IEnumerable<CourseDto>>();

        var count = await _unitOfWork.Courses.CountAsync();

        var criteria = new SearchCourseCriteriaDto
        {
            Name = request.Name,
            IsActive = request.IsActive,
            SortBy = request.SortBy,
            OrderBy = request.OrderBy
        };

        var courses = await _unitOfWork.Courses.GetPaginationAsync(request.PageNumber, request.PageSize, criteria);

        var data = from course in courses
                   select CourseMapper.MapCourseDto(course);

        response.PageNumber = request.PageNumber;
        response.TotalPages = (int)Math.Ceiling(count / (double)request.PageSize);
        response.TotalCount = count;
        response.Data = data;
        response.Success = true;
        response.Message = "Query succeed!";

        return response;
    }
}