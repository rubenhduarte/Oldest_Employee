using WebApplication1.ResultClass;
using WebApplication1.Services;

namespace OldestEmployee.Tests;

[TestFixture]
public class ExternalDataServiceTests
{
    // Implementación fake para simular IExternalDataService
    private class FakeExternalDataService : IExternalDataService
    {
        private readonly OperationResult<string> _result;
        public FakeExternalDataService(OperationResult<string> result)
        {
            _result = result;
        }
        public Task<OperationResult<string>> GetEmployeesJsonAsync()
        {
            return Task.FromResult(_result);
        }
    }

    [Test]
    public async Task GetEmployeesJsonAsync_ReturnsSuccessResult_WhenDataIsValid() {
        // Arrange: JSON válido
        string validJson = @"{
            ""status"": ""success"",
            ""data"": [
                { ""id"": 1, ""employee_name"": ""John Doe"", ""employee_salary"": 1000, ""employee_age"": 45, ""profile_image"": """" }
            ],
            ""message"": ""Success""
        }";
        var fakeService = new FakeExternalDataService(OperationResult<string>.CreateSuccess(validJson));

        // Act
        var result = await fakeService.GetEmployeesJsonAsync();

        // Assert
        Assert.That(result.Success,Is.True,"El resultado debería indicar éxito.");
        Assert.That(result.Data,Is.Not.Null,"El JSON no debe ser nulo.");
        Assert.That(result.Data,Does.Contain("\"status\""));
    }

    [Test]
    public async Task GetEmployeesJsonAsync_ReturnsFailureResult_WhenErrorOccurs() {
        // Arrange: Simular fallo
        var fakeService = new FakeExternalDataService(OperationResult<string>.CreateFailure("Service unavailable"));

        // Act
        var result = await fakeService.GetEmployeesJsonAsync();

        // Assert
        Assert.That(result.Success,Is.False,"El resultado debe indicar fallo.");
        Assert.That(result.ErrorMessage,Is.EqualTo("Service unavailable"));
    }
}