using System.Text.Json;
using WebApplication1.DtoResponse;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebApplication1.Services;

public class ExternalServices : IExternalServices
{

    public ExternalServices()
    {
       
    }

    public async Task<Employee> GetEmployeesAsync() {
        try 
        {

             // Configura las opciones de Chrome para ejecutarlo en modo headless.
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--disable-gpu");
            chromeOptions.AddArgument("--window-size=1920,1080");
            

             // Crea la instancia del driver.
            using (IWebDriver driver = new ChromeDriver(chromeOptions))
            {
                // Navega a la URL
                driver.Navigate().GoToUrl("http://dummy.restapiexample.com/api/v1/employees");

                // Espera unos segundos para que se ejecute el JavaScript.
                await Task.Delay(3000);

                // Obtén el contenido de la página (ya procesado)
                string pageSource = driver.PageSource;


                // Obtener el elemento <pre> que contiene el JSON
                var preElement = driver.FindElement(By.TagName("pre"));
                string jsonText = preElement.Text;


                var employeeResponse = JsonSerializer.Deserialize<EmployeeResponse>(jsonText, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if(employeeResponse?.Data == null || employeeResponse.Data.Count == 0) {
                    return null; // Si no hay empleados, devolvemos null
                }

                EmployeeResponse employeeResponseDto = new EmployeeResponse {
                    Data = new List<Employee>
                    {
                        new Employee
                        {
                            Id = 0,
                            EmployeeName = "",
                            EmployeeSalary = 0,
                            EmployeeAge = 0,
                            ProfileImage = ""
                        }
                    }
                };

                for(int i = 0;i < employeeResponse?.Data?.Count;i++) {
                    if(employeeResponse.Data[i].EmployeeAge > employeeResponseDto.Data[0].EmployeeAge) {
                        employeeResponseDto.Data[0].Id = employeeResponse.Data[i].Id;
                        employeeResponseDto.Data[0].EmployeeName = employeeResponse.Data[i].EmployeeName;
                        employeeResponseDto.Data[0].EmployeeSalary = employeeResponse.Data[i].EmployeeSalary;
                        employeeResponseDto.Data[0].EmployeeAge = employeeResponse.Data[i].EmployeeAge;
                        employeeResponseDto.Data[0].ProfileImage = employeeResponse.Data[i].ProfileImage;
                    }
                }

                return employeeResponseDto.Data[0];
            }
            return null;

        } catch(HttpRequestException httpEx) {
            Console.WriteLine($"Error HTTP al obtener los empleados: {httpEx.Message}");
            return null;
        } catch(Exception ex) {
            Console.WriteLine($"Error al obtener los empleados: {ex.Message}");
            return null;
        }
    }
}