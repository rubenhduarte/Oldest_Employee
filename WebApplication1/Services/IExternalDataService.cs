using WebApplication1.ResultClass;

namespace WebApplication1.Services;

public interface IExternalDataService 
{
    Task<OperationResult<string>> GetEmployeesJsonAsync();       
}
