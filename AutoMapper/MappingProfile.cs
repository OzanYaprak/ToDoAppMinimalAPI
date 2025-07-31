using AutoMapper;
using ToDoAppMinimalAPI.DTOs;

namespace ToDoAppMinimalAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskDTO, Task>().ReverseMap();
            CreateMap<TaskDTOForInsertion, Task>().ReverseMap();
            CreateMap<TaskDTOForUpdate, Task>().ReverseMap();
        }
    }
}
