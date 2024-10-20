using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Courses.Commands.UpdateCourseCommand;

public class UpdateCourseCommand : IRequest<BaseResponse<bool>>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; }
}