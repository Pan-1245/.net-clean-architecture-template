using CleanArchitecture.Interfaces.Mapper;
using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.Dto;
using CleanArchitecture.Master.Dto.SearchFilter;
using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Subjects.Queries.GetAllSubjectsAsPaginationQuery;

public class GetAllSubjectAsPaginationQueryHandler : IRequestHandler<GetAllSubjectAsPaginationQuery, BaseResponsePagination<IEnumerable<SubjectDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllSubjectAsPaginationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponsePagination<IEnumerable<SubjectDto>>> Handle(GetAllSubjectAsPaginationQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponsePagination<IEnumerable<SubjectDto>>();

        var count = await _unitOfWork.Subjects.CountAsync();

        var criteria = new SearchSubjectCriteriaDto
        {
            CourseId = request.CourseId,
            Name = request.Name,
            IsActive = request.IsActive,
            SortBy = request.SortBy,
            OrderBy = request.OrderBy
        };

        var subjects = await _unitOfWork.Subjects.GetPaginationAsync(request.PageNumber, request.PageSize, criteria);
        var courses = await _unitOfWork.Courses.GetAllAsync();

        var data = from subject in subjects
                   select SubjectMapper.MapSubjectDto(subject, courses);

        response.PageNumber = request.PageNumber;
        response.TotalPages = (int)Math.Ceiling(count / (double)request.PageSize);
        response.TotalCount = count;
        response.Data = data;
        response.Success = true;
        response.Message = "Query succeed!";

        return response;
    }
}