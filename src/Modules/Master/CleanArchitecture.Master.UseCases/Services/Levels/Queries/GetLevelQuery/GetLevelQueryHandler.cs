using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.Dto;
using CleanArchitecture.Master.UseCases.Common.Bases;
using CleanArchitecture.Master.UseCases.Mapper;
using CleanArchitecture.Utilities.Exceptions;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Levels.Queries.GetLevelQuery;

public class GetLevelQueryHandler : IRequestHandler<GetLevelQuery, BaseResponse<LevelDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLevelQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponse<LevelDto>> Handle(GetLevelQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<LevelDto>();

        var level = await _unitOfWork.Levels.GetAsync(request.Id);

        if (level is null)
        {
            throw new GeneralException.NotFound($"Level with given ID ({request.Id}) is not found.");
        }

        var courses = await _unitOfWork.Courses.GetAllAsync();

        var data = LevelMapper.MapLevelDto(level, courses);

        response.Data = data;
        response.Success = true;
        response.Message = "Query succeed!";

        return response;
    }
}