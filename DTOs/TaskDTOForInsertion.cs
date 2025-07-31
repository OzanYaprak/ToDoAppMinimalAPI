using System.Text.Json.Serialization;

namespace ToDoAppMinimalAPI.DTOs
{
    public record TaskDTOForInsertion : TaskDTO
    {
        [JsonIgnore]
        public new bool IsDeleted { get; init; } = false;
    }
}
