using Microsoft.AspNetCore.Mvc;
using _net_taskApiSimple.Models;
using _net_taskApiSimple.Services;

namespace _net_taskApiSimple.Controllers;

[ApiController]  // Bu attribute, sınıfın bir API controller olduğunu belirtir.
[Route("api/[controller]")]  // api/tasks
public class TasksController : ControllerBase
{
    private readonly TaskService _service;

    public TasksController(TaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var tasks = _service.GetTasks();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var task = _service.GetTaskById(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public IActionResult Create([FromBody] string title)
    {
        var task = _service.CreateTask(title);
        return Ok(task);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] TaskItem updatedTask)
    {
        var success = _service.UpdateTask(id, updatedTask.Title, updatedTask.IsCompleted);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var success = _service.DeleteTask(id);
        if (!success) return NotFound();

        return NoContent();
    }
}
