using CleanArchitecture.Master.Dto;
using CleanArchitecture.Master.UseCases.Common.Bases;
using MediatR;

namespace CleanArchitecture.Master.UseCases.Services.Levels.Queries.GetLevelQuery;

public class GetLevelQuery : IRequest<BaseResponse<LevelDto>>
{
    public Guid Id { get; set; }
}