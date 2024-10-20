using CleanArchitecture.Master.UseCases.Services.Subjects.Commands.CreateSubjectCommand;
using CleanArchitecture.Master.UseCases.Services.Subjects.Commands.DeleteSubjectCommand;
using CleanArchitecture.Master.UseCases.Services.Subjects.Commands.UpdateSubjectCommand;
using CleanArchitecture.Master.UseCases.Services.Subjects.Queries.GetAllSubjectsAsPaginationQuery;
using CleanArchitecture.Master.UseCases.Services.Subjects.Queries.GetAllSubjectsQuery;
using CleanArchitecture.Master.UseCases.Services.Subjects.Queries.GetSubjectQuery;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.PortalControllers.Master;

[ApiController]
[Route("api/master/[controller]")]
public class SubjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubjectsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetAllSubjectsQuery query)
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new GetSubjectQuery() { Id = id });

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("pagination")]
    public async Task<IActionResult> GetPaginationAsync([FromQuery] GetAllSubjectAsPaginationQuery query)
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
    public async Task<IActionResult> InsertAsync([FromBody] CreateSubjectCommand command)
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
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateSubjectCommand command)
    {
        if (command is null) return BadRequest();

        command.Id = id;

        var response = await _mediator.Send(command);

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new DeleteSubjectCommand() { Id = id });

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}