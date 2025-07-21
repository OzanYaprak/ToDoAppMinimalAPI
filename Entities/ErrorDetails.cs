using System.Text.Json;

namespace ToDoAppMinimalAPI.Entities
{
    public class ErrorDetails
    {
        public String? ErrorDate { get; set; }
        public String? Message { get; set; }
        public Int32 StatusCode { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
