using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Subjects.Commands.UpdateSubjectCommand;

public class UpdateSubjectCommand : IRequest<BaseResponse<bool>>
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; }
}