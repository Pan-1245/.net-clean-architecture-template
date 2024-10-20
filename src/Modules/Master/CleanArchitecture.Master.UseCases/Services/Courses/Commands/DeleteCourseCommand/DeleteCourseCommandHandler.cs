using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;
using CleanArchitecture.Utilities.Exceptions;

namespace CleanArchitecture.Master.UseCases.Services.Courses.Commands.DeleteCourseCommand;

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCourseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponse<bool>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        var course = await _unitOfWork.Courses.GetAsync(request.Id);

        if (course is null)
        {
            throw new GeneralException.NotFound($"Course with given ID ({request.Id}) is not found.");
        }

        response.Data = await _unitOfWork.Courses.DeleteAsync(request.Id);

        if (response.Data)
        {
            response.Success = true;
            response.Message = "Delete succeed!";
        }

        return response;
    }
}