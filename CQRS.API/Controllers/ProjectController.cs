using CQRS.API.Features.ProductFeatures.Queries;
using CQRS.API.Features.ProjectFeatures.Command;
using CQRS.API.Features.ProjectFeatures.Queries;
using CQRS.API.Models.Dtos.ProjectDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.API.Controllers;

[Route("api/project")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _mediator.Send(new GetProjectByIdQuery(id)));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _mediator.Send(new GetAllProjectByQuery()));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm]AddOrEditProjectDto addProjectDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(await _mediator.Send(new CreateProjectCommand(addProjectDto)));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, AddOrEditProjectDto editProjectDto)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (editProjectDto.Id != id)
            return BadRequest("Different ids");

        return Ok(await _mediator.Send(new UpdateProjectCommand(editProjectDto)));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("Id can't be null!");
        return Ok(await _mediator.Send(new DeleteProjectByIdCommand(id)));
    }
}
