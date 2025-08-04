using _net_taskApiSimple.Models;
using _net_taskApiSimple.Repositories;
using _net_taskApiSimple.DTOs;
using AutoMapper;
namespace _net_taskApiSimple.Services;
using _net_taskApiSimple.Interfaces;

public class TaskService : ITaskService
{
    // readonly olduğu için sadece constructor’da atanabilir ve sonrasında değiştirilemez.
    private readonly IMapper _mapper;
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public List<TaskResponseDto> GetTasks()
    {
        var tasks = _repository.GetAll();
        return _mapper.Map<List<TaskResponseDto>>(tasks);
    }



    public TaskResponseDto? GetTaskById(int id)
    {
        var task = _repository.GetById(id);
        if (task == null) return null;

        return _mapper.Map<TaskResponseDto>(task);
    }


    public List<TaskResponseDto> GetTasksByUser(int userId)
    {
        var tasks = _repository.GetAllByUserId(userId);
        return _mapper.Map<List<TaskResponseDto>>(tasks);
    }
    
    public TaskResponseDto CreateTask(string title, int userId)
    {
        var task = _repository.Add(title, userId);
        return _mapper.Map<TaskResponseDto>(task);
    }



    public bool UpdateTask(int id, string title, bool isCompleted, int userId)
    {
        var task = _repository.GetById(id);
        if (task == null || task.UserId != userId)
            return false;
    
        return _repository.Update(id, title, isCompleted);
    }
    
    public bool DeleteTask(int id, int userId)
    {
        var task = _repository.GetById(id);
        if (task == null || task.UserId != userId)
            return false;
    
        return _repository.Delete(id);
    }
    
}
