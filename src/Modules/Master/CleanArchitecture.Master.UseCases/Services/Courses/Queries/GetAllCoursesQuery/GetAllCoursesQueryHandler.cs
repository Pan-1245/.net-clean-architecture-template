using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.Dto;
using CleanArchitecture.Master.Dto.SearchFilter;
using CleanArchitecture.Master.UseCases.Common.Bases;
using CleanArchitecture.Master.UseCases.Mapper;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Courses.Queries.GetAllCoursesQuery;

public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, BaseResponse<IEnumerable<CourseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCoursesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponse<IEnumerable<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<CourseDto>>();

        var criteria = new SearchCourseCriteriaDto
        {
            Name = request.Name,
            IsActive = request.IsActive,
            SortBy = request.SortBy,
            OrderBy = request.OrderBy
        };

        var courses = await _unitOfWork.Courses.GetAllAsync(criteria);

        var data = from course in courses
                   select CourseMapper.MapCourseDto(course);

        response.Data = data;
        response.Success = true;
        response.Message = "Query succeed!";

        return response;
    }
}