namespace _net_taskApiSimple.DTOs;

public class UpdateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
}
