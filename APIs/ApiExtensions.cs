using System.ComponentModel.DataAnnotations;
using ToDoAppMinimalAPI.DTOs;
using ToDoAppMinimalAPI.Interfaces;

namespace ToDoAppMinimalAPI.APIs
{
    public static class ApiExtensions
    {
        public static void ToDoAppAPIs(this WebApplication app)
        {
            // Get All Tasks
            app.MapGet("/api/tasks", (IToDoAppService toDoAppService) =>
            {
                var tasks = toDoAppService.GetTasks();

                if (tasks == null || !tasks.Any())
                {
                    return Results.NoContent();
                }

                return Results.Ok(tasks);
            })
                .Produces<List<TaskDTO>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status204NoContent)
                .WithTags("CRUD", "GETs"); ;

            // Get Task by Id
            app.MapGet("/api/task/{id}", (int id,IToDoAppService toDoAppService) =>
            {
                var task = toDoAppService.GetTaskById(id);

                if (task == null)
                {
                    return Results.NoContent();
                }

                return Results.Ok(task);
            })
                .Produces<List<TaskDTO>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status204NoContent)
                .WithTags("CRUD", "GETs"); ;

            // Create Task
            app.MapPost("/api/task", (TaskDTOForInsertion taskDTOForInsertion, IToDoAppService toDoAppService) =>
            {
                var task = toDoAppService.CreateTask(taskDTOForInsertion);
                return Results.Created($"/api/books/{task.Id}", taskDTOForInsertion);
            })
                .Produces<Task>(StatusCodes.Status201Created) 
                .Produces<List<ValidationResult>>(StatusCodes.Status422UnprocessableEntity) 
                .WithTags("CRUD"); 
        }
    }
}
