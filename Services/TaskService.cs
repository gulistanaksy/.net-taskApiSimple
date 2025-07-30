using _net_taskApiSimple.Models;
using _net_taskApiSimple.Repositories;
using _net_taskApiSimple.DTOs;
namespace _net_taskApiSimple.Services;

public class TaskService
{
    // readonly olduğu için sadece constructor’da atanabilir ve sonrasında değiştirilemez.
    private readonly TaskRepository _repository;

    public TaskService(TaskRepository repository)
    {
        _repository = repository;
    }

    public List<TaskResponseDto> GetTasks()
    {
        // Her TaskItem öğesini TaskResponseDto'ya dönüştürür - 	Liste olarak döndürür
        return _repository.GetAll()
            .Select(task => new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                IsCompleted = task.IsCompleted
            }).ToList();
    }


    public TaskResponseDto? GetTaskById(int id)
    {
        var task = _repository.GetById(id);
        if (task == null) return null;

        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            IsCompleted = task.IsCompleted
        };
    }


    public TaskResponseDto CreateTask(string title)
    {
        var task = _repository.Add(title);
        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            IsCompleted = task.IsCompleted
        };
    }


    public bool UpdateTask(int id, string title, bool isCompleted)
    {
        return _repository.Update(id, title, isCompleted);
    }

    public bool DeleteTask(int id)
    {
        return _repository.Delete(id);
    }
}
