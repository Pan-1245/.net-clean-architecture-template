using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Courses.Commands.DeleteCourseCommand;

public class DeleteCourseCommand : IRequest<BaseResponse<bool>>
{
    public Guid Id { get; set; }
}