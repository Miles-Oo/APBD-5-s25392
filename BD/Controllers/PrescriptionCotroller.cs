using BD.Interface;
using Microsoft.AspNetCore.Mvc;
namespace BD;


[ApiController]
[Route("api/[controller]")]
public class PrescriptionController(IDbService service) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> AddPrescription()
    {
        try
        {
            return Ok( await service.GetPrescriptionsDetailsAsync());
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}