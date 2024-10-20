using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.UseCases.Common.Bases;
using CleanArchitecture.Utilities.Exceptions;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Subjects.Commands.UpdateSubjectCommand;

public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSubjectCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork)); ;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        var subject = await _unitOfWork.Subjects.GetAsync(request.Id);

        if (subject is null)
        {
            throw new GeneralException.NotFound($"Subject with given ID ({request.Id}) is not found.");
        }

        var course = await _unitOfWork.Courses.GetAsync(request.CourseId);

        if (course is null)
        {
            throw new GeneralException.NotFound($"Course with given ID ({request.CourseId}) is not found.");
        }

        subject.CourseId = request.CourseId;
        subject.Name = request.Name;
        subject.IsActive = request.IsActive;
        subject.UpdatedAt = DateTime.UtcNow;
        subject.UpdatedBy = "SYSTEM";

        response.Data = await _unitOfWork.Subjects.UpdateAsync(subject);

        if (response.Data)
        {
            response.Success = true;
            response.Message = "Update succeed!";
        }

        return response;
    }
}