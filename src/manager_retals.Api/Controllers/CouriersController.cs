using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CouriersController : ControllerBase
{

    public CouriersController()
    {
        
    }

    /// <summary>
    /// Register a new courier
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> RegisterCourier([FromBody] RegisterCourierRequest request)
    {
        // Call courier registration service
        return CreatedAtAction(nameof(GetById), new { id = 10 }, null);
    }

    /// <summary>
    /// Get courier by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        // Get courier by id
        return Ok(/*courier*/);
    }

    /// <summary>
    /// Update courier's driver's license image
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}/cnh")]
    public async Task<IActionResult> UpdateDriverLicense(Guid id, [FromForm] UpdateDriverLicenseRequest request)
    {
        // Update driver's license image
        return NoContent();
    }
}

public class UpdateDriverLicenseRequest
{
}

public class RegisterCourierRequest
{
}