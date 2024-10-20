using CleanArchitecture.Master.Dto;
using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Subjects.Queries.GetSubjectQuery;

public class GetSubjectQuery : IRequest<BaseResponse<SubjectDto>>
{
    public Guid Id { get; set; }
}