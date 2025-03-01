using System.Text.Json;
using WebApplication1.DtoResponse;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebApplication1.Services;

public class ExternalDataServices : IExternalDataServices
{

    public ExternalDataServices()
    {
       
    }

    public async Task<String> GetEmployeesJsonAsync() {
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


                return jsonText;
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