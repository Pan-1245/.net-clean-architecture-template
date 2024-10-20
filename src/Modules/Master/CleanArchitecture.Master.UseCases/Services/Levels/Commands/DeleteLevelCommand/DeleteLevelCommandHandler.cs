using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.UseCases.Common.Bases;
using CleanArchitecture.Utilities.Exceptions;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Levels.Commands.DeleteLevelCommand;

public class DeleteLevelCommandHandler : IRequestHandler<DeleteLevelCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLevelCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponse<bool>> Handle(DeleteLevelCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        var level = await _unitOfWork.Levels.GetAsync(request.Id);

        if (level is null)
        {
            throw new GeneralException.NotFound($"Level with given ID ({request.Id}) is not found.");
        }

        response.Data = await _unitOfWork.Levels.DeleteAsync(request.Id);

        if (response.Data)
        {
            response.Success = true;
            response.Message = "Delete succeed!";
        }

        return response;
    }
}