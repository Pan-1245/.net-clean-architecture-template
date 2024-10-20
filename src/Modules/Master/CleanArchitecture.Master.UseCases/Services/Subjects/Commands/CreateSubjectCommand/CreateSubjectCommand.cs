using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Subjects.Commands.CreateSubjectCommand;

public class CreateSubjectCommand : IRequest<BaseResponse<bool>>
{
    public Guid CourseId { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; }
}