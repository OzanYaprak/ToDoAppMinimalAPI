using AutoMapper;
using System.Net;
using ToDoAppMinimalAPI.DTOs;
using ToDoAppMinimalAPI.Entities;
using ToDoAppMinimalAPI.Exceptions;
using ToDoAppMinimalAPI.Interfaces;
using ToDoAppMinimalAPI.Repositories;
using ToDoAppMinimalAPI.Validators;

namespace ToDoAppMinimalAPI.Services
{
    public class ToDoAppService : IToDoAppService
    {
        #region Constructor

        private readonly CustomValidator _customValidator;
        private readonly ToDoAppRepository _toDoAppRepository;
        private readonly IMapper _mapper;
        public ToDoAppService(ToDoAppRepository toDoAppRepository, IMapper mapper, CustomValidator customValidator)
        {
            _toDoAppRepository = toDoAppRepository;
            _mapper = mapper;
            _customValidator = customValidator;
        }

        #endregion

        public List<TaskDTO> GetTasks()
        {
            var tasks = _toDoAppRepository.GetAll();

            return _mapper.Map<List<TaskDTO>>(tasks);
        }
        public TaskDTO GetTaskById(int id)
        {
            var task = _toDoAppRepository.Get(id);

            return _mapper.Map<TaskDTO>(task);
        }
        public Entities.Task CreateTask(TaskDTOForInsertion taskDTOForInsertion)
        {
            _customValidator.Validate(taskDTOForInsertion);

            var mappedTask = _mapper.Map<Entities.Task>(taskDTOForInsertion);

            if (mappedTask is null)
            {
                throw new TaskBadRequestException("Task could not be mapped from DTO to Entity.");
            }

            mappedTask.IsDeleted = false; // Varsayılan olarak IsDeleted özelliğini false olarak ayarlıyoruz.
            mappedTask.CreatedAt = DateTime.UtcNow; // Varsayılan olarak CreatedAt özelliğini UTC zaman diliminde ayarlıyoruz.
            mappedTask.IsCompleted = false; // Varsayılan olarak IsCompleted özelliğini false olarak ayarlıyoruz.

            _toDoAppRepository.Create(mappedTask);

            return mappedTask;

        }
        public Entities.Task UpdateTask(int id, TaskDTOForUpdate taskDTOForUpdate)
        {
            throw new NotImplementedException();
        }
        public void DeleteTask(int id)
        {
            throw new NotImplementedException();
        }
    }
}
