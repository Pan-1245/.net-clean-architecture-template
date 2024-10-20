using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;
using CleanArchitecture.Utilities.Exceptions;
using CleanArchitecture.Master.UseCases.Mapper;
using CleanArchitecture.Master.Dto;

namespace CleanArchitecture.Master.UseCases.Services.Courses.Queries.GetCourseQuery;

public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, BaseResponse<CourseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCourseQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponse<CourseDto>> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<CourseDto>();

        var course = await _unitOfWork.Courses.GetAsync(request.Id);

        if (course is null)
        {
            throw new GeneralException.NotFound($"Course with given ID ({request.Id}) was not found.");
        }

        var data = CourseMapper.MapCourseDto(course);

        response.Data = data;
        response.Success = true;
        response.Message = "Query succeed!";

        return response;
    }
}