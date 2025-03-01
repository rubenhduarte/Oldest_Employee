using WebApplication1.DtoResponse;

namespace WebApplication1.Services 
{
    public interface IExternalServices 
    {
        Task<Employee> GetEmployeesAsync();
        
    }
}
