using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Levels.Commands.DeleteLevelCommand;

public class DeleteLevelCommand : IRequest<BaseResponse<bool>>
{
    public Guid Id { get; set; }
}