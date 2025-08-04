using Microsoft.AspNetCore.Mvc;
using _net_taskApiSimple.Models;
using _net_taskApiSimple.Services;
using _net_taskApiSimple.DTOs;
using _net_taskApiSimple.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace _net_taskApiSimple.Controllers;

[ApiController]  // Bu attribute, sınıfın bir API controller olduğunu belirtir.
[Route("api/[controller]")]  // api/tasks
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;

    public TasksController(ITaskService service)
    {
        _service = service;
    }

    [Authorize]
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
    public IActionResult Create([FromBody] CreateTaskDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var task = _service.CreateTask(createDto.Title, createDto.UserId);
        return Ok(task);  
        // return CreatedAtAction(nameof(_service.GetTaskById), new { id = task.Id }, task);
    }


    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateTaskDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = _service.UpdateTask(id, updateDto.Title, updateDto.IsCompleted);
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
