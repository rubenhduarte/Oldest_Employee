using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IExternalServices _externalService;
        public EmployeeController(IExternalServices externalService)
        {
            _externalService = externalService;
        }

        [HttpGet("oldest-employee")]
        public async Task<IActionResult> GetOldestEmployee()
        {
            var employee = await _externalService.GetEmployeesAsync();
            if (employee == null)
                return NotFound("No se encontraron empleados.");

            return Ok(employee);
        }
    }
}
