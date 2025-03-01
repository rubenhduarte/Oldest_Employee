using WebApplication1.DtoResponse;

namespace WebApplication1.Services 
{
    public interface IExternalDataServices 
    {
        Task<String> GetEmployeesJsonAsync();        
    }
}
