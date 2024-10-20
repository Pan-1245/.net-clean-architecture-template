using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Levels.Commands.UpdateLevelCommand;

public class UpdateLevelCommand : IRequest<BaseResponse<bool>>
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; }
}