using System.Text.Json;
using WebApplication1.DtoResponse;

namespace WebApplication1.Services;

public class EmployeeProcessor : IEmployeeProcessor
{
    public Employee GetOldestEmployee(string jsonData)
    {
        if (string.IsNullOrEmpty(jsonData))
            throw new ArgumentException("El JSON no puede estar vacío", nameof(jsonData));

        var employeeResponse = JsonSerializer.Deserialize<EmployeeResponse>(jsonData, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Si no se deserializó correctamente o no hay datos, lanzamos excepción o retornamos null
        if (employeeResponse?.Data == null || employeeResponse.Data.Count == 0)
            throw new Exception("No se pudieron obtener datos de empleados");


        Employee oldestEmployee = new Employee 
        {       
            Id = 0,
            EmployeeName = "",
            EmployeeSalary = 0,
            EmployeeAge = 0,
            ProfileImage = ""
        };

        for(int i = 0;i < employeeResponse?.Data?.Count;i++) {
            if(employeeResponse.Data[i].EmployeeAge > oldestEmployee.EmployeeAge) 
            {
                oldestEmployee.Id = employeeResponse.Data[i].Id;
                oldestEmployee.EmployeeName = employeeResponse.Data[i].EmployeeName;
                oldestEmployee.EmployeeSalary = employeeResponse.Data[i].EmployeeSalary;
                oldestEmployee.EmployeeAge = employeeResponse.Data[i].EmployeeAge;
                oldestEmployee.ProfileImage = employeeResponse.Data[i].ProfileImage;
            }
        }

        return oldestEmployee;
    }
}
