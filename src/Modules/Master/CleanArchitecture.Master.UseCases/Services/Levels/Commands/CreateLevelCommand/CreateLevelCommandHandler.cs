using CleanArchitecture.Master.Domain.Entities;
using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.UseCases.Common.Bases;
using CleanArchitecture.Utilities.Exceptions;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Levels.Commands.CreateLevelCommand;

public class CreateLevelCommandHandler : IRequestHandler<CreateLevelCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateLevelCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponse<bool>> Handle(CreateLevelCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        var course = await _unitOfWork.Courses.GetAsync(request.CourseId);

        if (course is null)
        {
            throw new GeneralException.NotFound($"Course with given ID ({request.CourseId}) is not found.");
        }

        var level = new Level
        {
            CourseId = request.CourseId,
            Name = request.Name,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "SYSTEM",
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = "SYSTEM"
        };

        response.Data = await _unitOfWork.Levels.InsertAsync(level);

        if (response.Data)
        {
            response.Success = true;
            response.Message = "Create succeed!";
        }

        return response;
    }
}