namespace ToDoAppMinimalAPI.Exceptions.BaseExceptions
{
    public class NoContentException : Exception
    {
        protected NoContentException(string message) : base(message) { }
    }
}
