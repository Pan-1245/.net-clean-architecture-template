using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;
using CleanArchitecture.Utilities.Exceptions;

namespace CleanArchitecture.Master.UseCases.Services.Courses.Commands.UpdateCourseCommand;

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCourseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponse<bool>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        var course = await _unitOfWork.Courses.GetAsync(request.Id);

        if (course is null)
        {
            throw new GeneralException.NotFound($"Course with given ID ({request.Id}) is not found.");
        }

        course.Name = request.Name;
        course.IsActive = request.IsActive;
        course.UpdatedAt = DateTime.UtcNow;
        course.UpdatedBy = "SYSTEM";

        response.Data = await _unitOfWork.Courses.UpdateAsync(course);

        if (response.Data)
        {
            response.Success = true;
            response.Message = "Update succeed!";
        }

        return response;
    }
}