using manager_retals.Api.DTOs.Motorcycles;
using manager_retals.Core.Commands.Motorcycle;
using manager_retals.Core.Queries.Motorcycle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("motos")]
public class MotorcyclesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MotorcyclesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Register a new motorcycle
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost()]
    public async Task<IActionResult> RegisterMotorcycle([FromBody] RegisterMotorcycleRequest request)
    {
        var command = new CreateMotorcycleCommand(request.Identificador, request.Ano, request.Modelo, request.Placa);
        var motorcycleId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = motorcycleId }, null);
    }

    /// <summary>
    /// Get existing motorcycles.
    /// </summary>
    /// <param name="licensePlate"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> ListMotorcycles([FromQuery] string? plate)
    {
        var query = new GetAllMotorcyclesQuery(plate);
        var motorcycles = await _mediator.Send(query);
        return Ok(motorcycles);
    }

    /// <summary>
    /// Update the license plate of a motorcycle
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}/placa")]
    public async Task<IActionResult> UpdateLicensePlate(int id, [FromBody] UpdateLicensePlateRequest request)
    {
        var command = new UpdateMotorcycleCommand(id, request.Placa);
        var motorcycleId = await _mediator.Send(command);
        return Ok("Placa modificada com sucesso");
    }

    /// <summary>
    /// Get motorcycle by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        // Get motorcycle by id
        return Ok(/*motorcycle*/);
    }

    /// <summary>
    /// Remove a motorcycle
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveMotorcycle(int id)
    {
        var command = new RemoveMotorcycleCommand(id);
        var motorcycleId = await _mediator.Send(command);
        return Ok();
    }
}