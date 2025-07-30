using AutoMapper;
using _net_taskApiSimple.DTOs;
using _net_taskApiSimple.Models;

namespace _net_taskApiSimple.Mappings;

public class TaskMappingProfile : Profile
{
    public TaskMappingProfile()
    {
        CreateMap<TaskItem, TaskResponseDto>();
    }
}
// Bu sınıf: TaskItem → TaskResponseDto dönüşümünü nasıl yapacağını tanımlar.
