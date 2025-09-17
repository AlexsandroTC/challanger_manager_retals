using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class RentalsController : ControllerBase
{
    // Injete os serviços necessários via construtor
    public RentalsController()
    {

    }
    // <summary>
    /// Rent a motorcycle
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> RentMotorcycle([FromBody] RentMotorcycleRequest request)
    {
        // Call rental service
        return CreatedAtAction(nameof(GetById), new { id = 10 }, null);
    }

    /// <summary>
    /// Return a motorcycle and calculate rental values
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}/return")]
    public async Task<IActionResult> ReturnMotorcycle(Guid id, [FromBody] ReturnMotorcycleRequest request)
    {
        // Process return and calculate values
        return Ok(/*return summary*/);
    }

    /// <summary>
    /// Get rental by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        // Get rental by id
        return Ok(/*rental*/);
    }
}

public class ReturnMotorcycleRequest
{
}

public class RentMotorcycleRequest
{
}