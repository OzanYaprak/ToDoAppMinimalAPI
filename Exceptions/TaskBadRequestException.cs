using ToDoAppMinimalAPI.Exceptions.BaseExceptions;

namespace ToDoAppMinimalAPI.Exceptions
{
    public sealed class TaskBadRequestException : BadRequestException
    {
        public TaskBadRequestException(Task task) : base($"Bad Request")
        {
        }
        public TaskBadRequestException() : base($"Bad Request")
        {
        }
        public TaskBadRequestException(string message) : base(message)
        {
        }
    }
}
