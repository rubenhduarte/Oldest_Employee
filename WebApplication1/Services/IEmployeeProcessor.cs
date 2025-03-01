using WebApplication1.DtoResponse;

namespace WebApplication1.Services;

public interface IEmployeeProcessor 
{
    Employee GetOldestEmployee(string jsonData);
}

