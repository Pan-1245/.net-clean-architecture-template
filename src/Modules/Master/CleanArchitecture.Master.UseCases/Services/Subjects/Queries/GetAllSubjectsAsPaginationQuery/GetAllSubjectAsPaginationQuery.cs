using CleanArchitecture.Master.Dto;
using CleanArchitecture.Master.UseCases.Common.Bases;
using CleanArchitecture.Shared.Domain.Enum;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Subjects.Queries.GetAllSubjectsAsPaginationQuery;

public class GetAllSubjectAsPaginationQuery : IRequest<BaseResponsePagination<IEnumerable<SubjectDto>>>
{
    public Guid? CourseId { get; set; }
    public string? Name { get; set; }
    public bool? IsActive { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; }
    public SortingOrder? OrderBy { get; set; } = SortingOrder.ASC;
}