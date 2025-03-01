using System.Text.Json;
using WebApplication1.DtoResponse;
using WebApplication1.Services;

namespace OldestEmployee.Tests;
[TestFixture]
public class EmployeeProcessorTests {
    private IEmployeeProcessor _processor;

    [SetUp]
    public void Setup() {
        _processor = new EmployeeProcessor();
    }

    [Test]
    public void GetOldestEmployee_ValidJson_ReturnsCorrectEmployee() {
        // Arrange: JSON con dos empleados
        string json = @"{
                    ""status"": ""success"",
                    ""data"": [
                        { ""id"": 1, ""employee_name"": ""Alice"", ""employee_salary"": 1000, ""employee_age"": 30, ""profile_image"": """" },
                        { ""id"": 2, ""employee_name"": ""Bob"", ""employee_salary"": 2000, ""employee_age"": 50, ""profile_image"": """" }
                    ],
                    ""message"": ""Success""
                }";

        // Act
        Employee oldestEmployee = _processor.GetOldestEmployee(json);

        // Assert
       
        Assert.That(oldestEmployee, Is.Not.Null, "El empleado obtenido no debe ser nulo.");
        Assert.That(oldestEmployee.Id, Is.EqualTo(2), "Se esperaba que el empleado con id 2 fuera el más viejo.");
        Assert.That(oldestEmployee.EmployeeName, Is.EqualTo("Bob"));
    }

    [Test]
    public void GetOldestEmployee_InvalidJson_ThrowsException() {
        // Arrange: JSON inválido
        string invalidJson = "invalid json";

        // Act & Assert
        Assert.Throws<JsonException>(() => _processor.GetOldestEmployee(invalidJson));
    }
}
