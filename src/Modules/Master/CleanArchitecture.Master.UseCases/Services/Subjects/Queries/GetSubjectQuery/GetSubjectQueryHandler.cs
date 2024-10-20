using CleanArchitecture.Interfaces.Mapper;
using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.Dto;
using CleanArchitecture.Master.UseCases.Common.Bases;
using CleanArchitecture.Utilities.Exceptions;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Subjects.Queries.GetSubjectQuery;

public class GetSubjectQueryHandler : IRequestHandler<GetSubjectQuery, BaseResponse<SubjectDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSubjectQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponse<SubjectDto>> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<SubjectDto>();

        var subject = await _unitOfWork.Subjects.GetAsync(request.Id);

        if (subject is null)
        {
            throw new GeneralException.NotFound($"Subject with given ID ({request.Id}) is not found.");
        }

        var courses = await _unitOfWork.Courses.GetAllAsync();

        var data = SubjectMapper.MapSubjectDto(subject, courses);

        response.Data = data;
        response.Success = true;
        response.Message = "Query succeed!";

        return response;
    }
}