using CQRS.API.Features.CountryFeatures.Command;
using CQRS.API.Features.CountryFeatures.Queries;
using CQRS.API.Features.ProductFeatures.Command;
using CQRS.API.Features.ProductFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.API.Controllers;

[Route("api/country")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CountryController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create(CreateCountryCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllCountryQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _mediator.Send(new GetCountryByIdQuery { Id = id }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _mediator.Send(new DeleteCountryByIdCommand { Id = id }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCountryCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        return Ok(await _mediator.Send(command));
    }
}