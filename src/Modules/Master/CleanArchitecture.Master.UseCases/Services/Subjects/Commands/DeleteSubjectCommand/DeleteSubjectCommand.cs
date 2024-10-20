using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Subjects.Commands.DeleteSubjectCommand;

public class DeleteSubjectCommand : IRequest<BaseResponse<bool>>
{
    public Guid Id { get; set; }
}