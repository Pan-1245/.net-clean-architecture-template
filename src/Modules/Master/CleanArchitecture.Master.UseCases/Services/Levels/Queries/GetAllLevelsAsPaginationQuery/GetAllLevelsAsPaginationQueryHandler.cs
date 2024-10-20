using CleanArchitecture.Interfaces.Persistence;
using CleanArchitecture.Master.Dto;
using CleanArchitecture.Master.Dto.SearchFilter;
using CleanArchitecture.Master.UseCases.Common.Bases;
using CleanArchitecture.Master.UseCases.Mapper;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Levels.Queries.GetAllLevelsAsPaginationQuery;

public class GetAllLevelsAsPaginationQueryHandler : IRequestHandler<GetAllLevelsAsPaginationQuery, BaseResponsePagination<IEnumerable<LevelDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllLevelsAsPaginationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponsePagination<IEnumerable<LevelDto>>> Handle(GetAllLevelsAsPaginationQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponsePagination<IEnumerable<LevelDto>>();

        var count = await _unitOfWork.Levels.CountAsync();

        var criteria = new SearchLevelCriteriaDto
        {
            CourseId = request.CourseId,
            Name = request.Name,
            IsActive = request.IsActive,
            SortBy = request.SortBy,
            OrderBy = request.OrderBy
        };

        var levels = await _unitOfWork.Levels.GetPaginationAsync(request.PageNumber, request.PageSize, criteria);
        var courses = await _unitOfWork.Courses.GetAllAsync();

        var data = from level in levels
                   select LevelMapper.MapLevelDto(level, courses);

        response.PageNumber = request.PageNumber;
        response.TotalPages = (int)Math.Ceiling(count / (double)request.PageSize);
        response.TotalCount = count;
        response.Data = data;
        response.Success = true;
        response.Message = "Query succeed!";

        return response;
    }
}