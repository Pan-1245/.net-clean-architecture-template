using CleanArchitecture.Master.UseCases.Services.Levels.Commands.CreateLevelCommand;
using CleanArchitecture.Master.UseCases.Services.Levels.Commands.DeleteLevelCommand;
using CleanArchitecture.Master.UseCases.Services.Levels.Commands.UpdateLevelCommand;
using CleanArchitecture.Master.UseCases.Services.Levels.Queries.GetAllLevelsAsPaginationQuery;
using CleanArchitecture.Master.UseCases.Services.Levels.Queries.GetAllLevelsQuery;
using CleanArchitecture.Master.UseCases.Services.Levels.Queries.GetLevelQuery;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.PortalControllers.Master;

[ApiController]
[Route("api/master/[controller]")]
public class LevelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LevelsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _mediator.Send(new GetAllLevelsQuery());

        if (response.Success)
        {
            if (response.Data is null || !response.Data.Any())
            {
                return NoContent();
            }

            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new GetLevelQuery() { Id = id });

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("pagination")]
    public async Task<IActionResult> GetPaginationAsync([FromQuery] GetAllLevelsAsPaginationQuery query)
    {
        var response = await _mediator.Send(query);

        if (response.Success)
        {
            if (response.Data is null || !response.Data.Any())
            {
                return NoContent();
            }

            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] CreateLevelCommand command)
    {
        if (command is null) return BadRequest();

        var response = await _mediator.Send(command);

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateLevelCommand command)
    {
        if (command is null) return BadRequest();

        command.Id = id;

        var response = await _mediator.Send(command);

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new DeleteLevelCommand() { Id = id });

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}