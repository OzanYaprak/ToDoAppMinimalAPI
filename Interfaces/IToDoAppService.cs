using ToDoAppMinimalAPI.DTOs;

namespace ToDoAppMinimalAPI.Interfaces
{
    public interface IToDoAppService
    {
        List<TaskDTO> GetTasks();
        TaskDTO GetTaskById(int id);
        Entities.Task CreateTask(TaskDTOForInsertion taskDTOForInsertion);
        Entities.Task UpdateTask(int id, TaskDTOForUpdate taskDTOForUpdate);
        void DeleteTask(int id);
    }
}
