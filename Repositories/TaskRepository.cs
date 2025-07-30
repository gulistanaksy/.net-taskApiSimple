using _net_taskApiSimple.Models;

namespace _net_taskApiSimple.Repositories;

//Veritabanı yok, sadece runtime süresince çalışan geçici bir liste kullanılır.
public class TaskRepository
{
    private readonly List<TaskItem> _tasks = []; // readonly: Bu liste referansı değiştirilemez ama içeriği değiştirilebilir.

    public List<TaskItem> GetAll()
    {
        return _tasks;
    }

    public TaskItem? GetById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public TaskItem Add(string title)
    {
        var task = new TaskItem
        {
            Id = _tasks.Count + 1,
            Title = title
        };

        _tasks.Add(task);
        return task;
    }

    public bool Update(int id, string title, bool isCompleted)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return false;

        task.Title = title;
        task.IsCompleted = isCompleted;
        return true;
    }

    public bool Delete(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return false;

        _tasks.Remove(task);
        return true;
    }
}
