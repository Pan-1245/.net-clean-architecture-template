using CleanArchitecture.Master.Dto;
using CleanArchitecture.Master.UseCases.Common.Bases;
using CleanArchitecture.Shared.Domain.Enum;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Subjects.Queries.GetAllSubjectsQuery;

public class GetAllSubjectsQuery : IRequest<BaseResponse<IEnumerable<SubjectDto>>>
{
    public Guid? CourseId { get; set; }
    public string? Name { get; set; }
    public bool? IsActive { get; set; }
    public string? SortBy { get; set; }
    public SortingOrder? OrderBy { get; set; } = SortingOrder.ASC;
}