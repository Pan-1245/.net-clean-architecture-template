using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Courses.Commands.CreateCourseCommand;

public class CreateCourseCommand : IRequest<BaseResponse<bool>>
{
    public required string Name { get; set; }
    public bool IsActive { get; set; }
}