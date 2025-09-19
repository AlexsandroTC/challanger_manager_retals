using manager_retals.Api.DTOs.Driver;
using manager_retals.Core.Commands.Driver;
using manager_retals.Core.Commands.Motorcycle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class DriverController : ControllerBase
{
    private readonly IMediator _mediator;

    public DriverController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Register a new driver
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> RegisterDriver([FromBody] RegisterDriverRequest request)
    {
        // Call courier registration service
        var command = new CreateDriverCommand(request.Identificador, request.Name, request.Cnpj, request.BirthDate, request.CnhNumber, request.CnhType, request.CnhImage);
        await _mediator.Send(command);
        return Created();
    }

    /// <summary>
    /// Update courier's driver's license image
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}/cnh")]
    public async Task<IActionResult> UpdateDriverLicense(int id, [FromForm] UpdateDriverLicenseRequest request)
    {
        // Update driver's license image
        return NoContent();
    }
}