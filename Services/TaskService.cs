using _net_taskApiSimple.Models;
using _net_taskApiSimple.Repositories;

namespace _net_taskApiSimple.Services;

public class TaskService
{
    // readonly olduğu için sadece constructor’da atanabilir ve sonrasında değiştirilemez.
    private readonly TaskRepository _repository;

    public TaskService(TaskRepository repository)
    {
        _repository = repository;
    }

    public List<TaskItem> GetTasks()
    {
        return _repository.GetAll();
    }

    public TaskItem? GetTaskById(int id)
    {
        return _repository.GetById(id);
    }

    public TaskItem CreateTask(string title)
    {
        return _repository.Add(title);
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
