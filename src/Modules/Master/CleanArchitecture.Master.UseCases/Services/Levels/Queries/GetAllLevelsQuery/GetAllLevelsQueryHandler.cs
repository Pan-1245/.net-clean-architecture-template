using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.Dto;
using CleanArchitecture.Master.Dto.SearchFilter;
using CleanArchitecture.Master.UseCases.Common.Bases;
using CleanArchitecture.Master.UseCases.Mapper;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Levels.Queries.GetAllLevelsQuery;

public class GetAllLevelsQueryHandler : IRequestHandler<GetAllLevelsQuery, BaseResponse<IEnumerable<LevelDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllLevelsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponse<IEnumerable<LevelDto>>> Handle(GetAllLevelsQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<LevelDto>>();

        var criteria = new SearchLevelCriteriaDto
        {
            CourseId = request.CourseId,
            Name = request.Name,
            IsActive = request.IsActive,
            SortBy = request.SortBy,
            OrderBy = request.OrderBy
        };

        var levels = await _unitOfWork.Levels.GetAllAsync(criteria);
        var courses = await _unitOfWork.Courses.GetAllAsync();

        var data = from level in levels
                   orderby level.CourseId
                   select LevelMapper.MapLevelDto(level, courses);

        response.Data = data;
        response.Success = true;
        response.Message = "Query succeed!";

        return response;
    }
}