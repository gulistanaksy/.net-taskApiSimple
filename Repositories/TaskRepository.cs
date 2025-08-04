using _net_taskApiSimple.Models;
using _net_taskApiSimple.Data;
using Microsoft.EntityFrameworkCore;
using _net_taskApiSimple.Interfaces;

namespace _net_taskApiSimple.Repositories;

public class TaskRepository : ITaskRepository

{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<TaskItem> GetAll()
    {
        return _context.Tasks
            .Include(t => t.User) // kullanıcıyı da çek
            .ToList();
    }
    
    public TaskItem? GetById(int id)
    {
        return _context.Tasks
            .Include(t => t.User)
            .FirstOrDefault(t => t.Id == id);
    }
    public List<TaskItem> GetAllByUserId(int userId)
    {
        return _context.Tasks
            .Include(t => t.User)
            .Where(t => t.UserId == userId)
            .ToList();
    }

    public TaskItem Add(string title, int userId)
    {
        var task = new TaskItem
        {
            Title = title,
            IsCompleted = false,
            UserId = userId
        };

        _context.Tasks.Add(task);
        _context.SaveChanges();

        return task;
    }


    public bool Update(int id, string title, bool isCompleted)
    {
        var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return false;

        task.Title = title;
        task.IsCompleted = isCompleted;
        _context.SaveChanges();

        return true;
    }

    public bool Delete(int id)
    {
        var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return false;

        _context.Tasks.Remove(task);
        _context.SaveChanges();

        return true;
    }
}
