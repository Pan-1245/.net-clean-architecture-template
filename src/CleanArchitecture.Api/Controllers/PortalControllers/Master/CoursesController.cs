using CleanArchitecture.Master.UseCases.Services.Courses.Commands.CreateCourseCommand;
using CleanArchitecture.Master.UseCases.Services.Courses.Commands.DeleteCourseCommand;
using CleanArchitecture.Master.UseCases.Services.Courses.Commands.UpdateCourseCommand;
using CleanArchitecture.Master.UseCases.Services.Courses.Queries.GetAllCoursesAsPaginationQuery;
using CleanArchitecture.Master.UseCases.Services.Courses.Queries.GetAllCoursesQuery;
using CleanArchitecture.Master.UseCases.Services.Courses.Queries.GetCourseQuery;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.PortalControllers.Master;

[ApiController]
[Route("api/master/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _mediator.Send(new GetAllCoursesQuery());

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
        var response = await _mediator.Send(new GetCourseQuery() { Id = id });

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("pagination")]
    public async Task<IActionResult> GetPaginationAsync([FromQuery] GetAllCoursesAsPaginationQuery query)
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
    public async Task<IActionResult> InsertAsync([FromBody] CreateCourseCommand command)
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
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateCourseCommand command)
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
        var response = await _mediator.Send(new DeleteCourseCommand() { Id = id });

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}