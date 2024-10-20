using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.UseCases.Common.Bases;
using CleanArchitecture.Utilities.Exceptions;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Subjects.Commands.DeleteSubjectCommand;

public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSubjectCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponse<bool>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        var subject = await _unitOfWork.Subjects.GetAsync(request.Id);

        if (subject is null)
        {
            throw new GeneralException.NotFound($"Subject with given ID ({request.Id}) is not found.");
        }

        response.Data = await _unitOfWork.Subjects.DeleteAsync(request.Id);

        if (response.Data)
        {
            response.Success = true;
            response.Message = "Delete succeed!";
        }

        return response;
    }
}