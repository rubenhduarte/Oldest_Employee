using WebApplication1.Services;
using Xunit;

namespace TestProject 
{
    public class ExternalDataServiceTests
    {
        // Prueba de integración real: verifica que se retorna un JSON con la clave "status".
        // Nota: Esta prueba depende del servicio externo y puede fallar si el servicio no responde.
        [Fact]
        public async Task GetEmployeeJsonAsync_ReturnsJsonString()
        {
            // Arrange
            IExternalDataServices service = new ExternalDataServices();

            // Act
            string json = await service.GetEmployeesJsonAsync();

            // Assert
            Assert.False(string.IsNullOrEmpty(json));
            Assert.Contains("\"status\"", json);
        }

        // Prueba de fallo: usando una implementación fake que simula un error.
        [Fact]
        public async Task GetEmployeeJsonAsync_WhenServiceFails_ThrowsException()
        {
            // Arrange
            IExternalDataServices service = new ExternalDataServiceFailure();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.GetEmployeeJsonAsync());
        }
    }

    // Implementación "fake" para simular un fallo.
    public class ExternalDataServiceFailure : IExternalDataServices
    {
        public Task<string> GetEmployeeJsonAsync()
        {
            throw new Exception("Simulated failure");
        }
    }
}
