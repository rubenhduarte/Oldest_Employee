using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApplication1.Controllers;
using WebApplication1.DtoResponse;
using WebApplication1.ResultClass;
using WebApplication1.Services;

namespace OldestEmployee.Tests;

[TestFixture]
public class EmployeeControllerTests {
    private Mock<IExternalDataService> _mockExternalDataService;
    private Mock<IEmployeeProcessor> _mockEmployeeProcessor;
    private EmployeeController _controller;

    [SetUp]
    public void Setup() {
        _mockExternalDataService = new Mock<IExternalDataService>();
        _mockEmployeeProcessor = new Mock<IEmployeeProcessor>();
        _controller = new EmployeeController(_mockExternalDataService.Object,_mockEmployeeProcessor.Object);
    }

    [Test]
    public async Task GetOldestEmployee_ReturnsOkResult_WithEmployee() {
        // Arrange
        string dummyJson = @"{
            ""status"": ""success"",
            ""data"": [
                { ""id"": 1, ""employee_name"": ""Alice"", ""employee_salary"": 1000, ""employee_age"": 30, ""profile_image"": """" }
            ],
            ""message"": ""Success""
        }";
        var expectedEmployee = new Employee {
            Id = 1,
            EmployeeName = "Alice",
            EmployeeSalary = 1000,
            EmployeeAge = 30,
            ProfileImage = ""
        };

        _mockExternalDataService.Setup(s => s.GetEmployeesJsonAsync())
            .ReturnsAsync(OperationResult<string>.CreateSuccess(dummyJson));

        _mockEmployeeProcessor.Setup(p => p.GetOldestEmployee(dummyJson))
            .Returns(expectedEmployee);

        // Act
        IActionResult actionResult = await _controller.GetOldestEmployee();

        // Assert
        Assert.That(actionResult,Is.InstanceOf<OkObjectResult>(),"El resultado debería ser Ok.");
        var okResult = actionResult as OkObjectResult;
        Assert.That(okResult,Is.Not.Null);
        Assert.That(okResult.Value,Is.EqualTo(expectedEmployee));
    }

    [Test]
    public async Task GetOldestEmployee_ReturnsNotFound_WhenExternalServiceFails() {
        // Arrange: simular fallo del servicio externo
        _mockExternalDataService.Setup(s => s.GetEmployeesJsonAsync())
            .ReturnsAsync(OperationResult<string>.CreateFailure("Service unavailable"));

        // Act
        IActionResult actionResult = await _controller.GetOldestEmployee();

        // Assert
        Assert.That(actionResult,Is.InstanceOf<NotFoundObjectResult>(),"Se espera NotFound cuando falla el servicio externo.");
    }

    [Test]
    public async Task GetOldestEmployee_ReturnsNotFound_WhenEmployeeProcessingFails() {
        // Arrange: el servicio externo retorna JSON válido, pero el procesador lanza excepción
        string dummyJson = @"{
            ""status"": ""success"",
            ""data"": [],
            ""message"": ""No records found""
        }";

        _mockExternalDataService.Setup(s => s.GetEmployeesJsonAsync())
            .ReturnsAsync(OperationResult<string>.CreateSuccess(dummyJson));

        _mockEmployeeProcessor.Setup(p => p.GetOldestEmployee(dummyJson))
            .Throws(new System.Exception("No employee found"));

        // Act
        IActionResult actionResult = await _controller.GetOldestEmployee();

        // Assert
        Assert.That(actionResult,Is.InstanceOf<NotFoundObjectResult>(),"Se espera NotFound cuando el procesamiento falla.");
    }
}
