using Microsoft.AspNetCore.Mvc;
using WebApplication1.DtoResponse;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IExternalDataServices _externalDataService;
        private readonly IEmployeeProcessor _employeeProcessor;

        public EmployeeController(IExternalDataServices externalDataService,
                                  IEmployeeProcessor employeeProcessor)
        {
            _externalDataService = externalDataService;
            _employeeProcessor = employeeProcessor;
        }

        [HttpGet("oldest-employee")]
        public async Task<IActionResult> GetOldestEmployee()
        {

            // Obtener la cadena JSON del servicio externo
            string jsonData = await _externalDataService.GetEmployeesJsonAsync();

            // Procesar el JSON para obtener el empleado más viejo
            Employee oldestEmployee = _employeeProcessor.GetOldestEmployee(jsonData);

            return Ok(oldestEmployee);
        }
    }
}
