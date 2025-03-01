using System.Text.Json.Serialization;

namespace WebApplication1.DtoResponse {
    public class EmployeeResponse 
        
    {
         [JsonPropertyName("status")]
         public string? Status { get; set; }
         [JsonPropertyName("data")]
         public List<Employee>? Data { get; set; }
         
         [JsonPropertyName("message")]
         public string? Message { get; set; }
    }
}
