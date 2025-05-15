using BD.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController(IDbService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        try
        {
            var result = await service.GetPatientDetailsAsync(id);
            if (result == null)
            {
                return NotFound(new { error = "Brak pacjenta" });
            }

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}