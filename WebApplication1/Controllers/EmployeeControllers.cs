using Microsoft.AspNetCore.Mvc;
using WebApplication1.DtoResponse;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IExternalDataService _externalDataService;
        private readonly IEmployeeProcessor _employeeProcessor;

        public EmployeeController(IExternalDataService externalDataService,
                                  IEmployeeProcessor employeeProcessor)
        {
            _externalDataService = externalDataService;
            _employeeProcessor = employeeProcessor;
        }

        [HttpGet("oldest-employee")]
        public async Task<IActionResult> GetOldestEmployee()
        {

            // Obtener el resultado de la operación que contiene el JSON.
            var result = await _externalDataService.GetEmployeesJsonAsync();

            // Verificar si la operación fue exitosa.
            if (!result.Success)
            {
                // Puedes devolver NotFound o BadRequest, según lo que consideres adecuado.
                return NotFound(result.ErrorMessage);
            }

            // Extraer el JSON real.
            string jsonData = result.Data;

            try
            { 
                // Procesar el JSON para obtener el empleado más viejo.
                Employee oldestEmployee = _employeeProcessor.GetOldestEmployee(jsonData);
                return Ok(oldestEmployee);
            }
            catch (Exception ex)
            {
                // Si ocurre cualquier error en el procesamiento, devolvemos NotFound con el mensaje.
                return NotFound(ex.Message);
            }
           
        }
    }
}
