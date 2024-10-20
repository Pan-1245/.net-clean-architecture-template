using CleanArchitecture.Master.Domain.Entities;
using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Courses.Commands.CreateCourseCommand;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponse<bool>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        var course = new Course
        {
            Name = request.Name,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "SYSTEM",
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = "SYSTEM"
        };

        response.Data = await _unitOfWork.Courses.InsertAsync(course);

        if (response.Data)
        {
            response.Success = true;
            response.Message = "Create succeed!";
        }

        return response;
    }
}