using AutoMapper;
using _net_taskApiSimple.Models;
using _net_taskApiSimple.DTOs;

namespace _net_taskApiSimple.Mappings;

public class TaskMappingProfile : Profile
{
    public TaskMappingProfile()
    {
        CreateMap<TaskItem, TaskResponseDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User!.Username));

        CreateMap<User, UserResponseDto>();
    }
}

// Bu sınıf: TaskItem → TaskResponseDto dönüşümünü nasıl yapacağını tanımlar.
// modelden username aldı ve dto içine ekledi.