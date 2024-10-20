using CleanArchitecture.Interfaces.Mapper;
using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.Dto;
using CleanArchitecture.Master.Dto.SearchFilter;
using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Subjects.Queries.GetAllSubjectsQuery;

public class GetAllSubjectsQueryHandler : IRequestHandler<GetAllSubjectsQuery, BaseResponse<IEnumerable<SubjectDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllSubjectsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork)); ;
    }

    public async Task<BaseResponse<IEnumerable<SubjectDto>>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SubjectDto>>();

        var criteria = new SearchSubjectCriteriaDto
        {
            CourseId = request.CourseId,
            Name = request.Name,
            IsActive = request.IsActive,
            SortBy = request.SortBy,
            OrderBy = request.OrderBy
        };

        var subjects = await _unitOfWork.Subjects.GetAllAsync(criteria);
        var courses = await _unitOfWork.Courses.GetAllAsync();

        var data = from subject in subjects
                   orderby subject.CourseId
                   select SubjectMapper.MapSubjectDto(subject, courses);

        response.Data = data;
        response.Success = true;
        response.Message = "Query succeed!";

        return response;
    }
}