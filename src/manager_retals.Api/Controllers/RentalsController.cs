using manager_retals.Api.DTOs.Rental;
using manager_retals.Core.Commands.Rental;
using manager_retals.Core.Queries.Rental;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class RentalsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RentalsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new motorcycle rental based on the specified request details.
    /// </summary>
    /// <param name="request">The rental request containing the motorcycle, driver, rental plan, and rental period information. Cannot be
    /// null.</param>
    /// <returns>A 201 Created response if the rental is successfully created.</returns>
    [HttpPost]
    public async Task<IActionResult> RentMotorcycle([FromBody] RentMotorcycleRequest request)
    {
        var command = new CreateRentalCommand(request.MotorcycleId, request.DriverId, request.Plan, request.StartDate, request.EndDate, request.ExpectedEndDate);
        await _mediator.Send(command);
        return Created();
    }

    /// <summary>
    /// Return a motorcycle and calculate rental values
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}/return")]
    public async Task<IActionResult> ReturnMotorcycle(int id, [FromBody] ReturnRentalRequest request)
    {
        var command = new ReturnRentalCommand(id, request.ReturnDate);
        await _mediator.Send(command);
        return Ok();
    }

    /// <summary>
    /// Get rental by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetRentalsQuery(id);
        var rentals = await _mediator.Send(query);
        return Ok(rentals);
    }
}