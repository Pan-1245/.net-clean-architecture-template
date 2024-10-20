using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.UseCases.Common.Bases;
using CleanArchitecture.Utilities.Exceptions;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Levels.Commands.UpdateLevelCommand;

public class UpdateLevelCommandHandler : IRequestHandler<UpdateLevelCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLevelCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponse<bool>> Handle(UpdateLevelCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        var level = await _unitOfWork.Levels.GetAsync(request.Id);

        if (level is null)
        {
            throw new GeneralException.NotFound($"Level with given ID ({request.Id}) is not found.");
        }

        level.CourseId = request.CourseId;
        level.Name = request.Name;
        level.IsActive = request.IsActive;
        level.UpdatedAt = DateTime.UtcNow;
        level.UpdatedBy = "SYSTEM";

        response.Data = await _unitOfWork.Levels.UpdateAsync(level);

        if (response.Data)
        {
            response.Success = true;
            response.Message = "Update succeed!";
        }

        return response;
    }
}