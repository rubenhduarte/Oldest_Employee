using System.Text.Json.Serialization;

namespace WebApplication1.DtoResponse 
{
    public class Employee 
    {
        public int Id {
            get;
            set;
        }

         [JsonPropertyName("employee_name")]
        public  string EmployeeName {
            get;
            set;
        }
        [JsonPropertyName("employee_salary")]
        public int EmployeeSalary {
            get;
            set;
        }

        [JsonPropertyName("employee_age")]
        public int EmployeeAge {
            get;
            set;
        }
        [JsonPropertyName("profile_image")]
        public string ProfileImage {
            get;
            set;
        }

    }
}

