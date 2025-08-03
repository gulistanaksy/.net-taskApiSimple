namespace _net_taskApiSimple.DTOs;

public class TaskResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
    public int userId { get; set; }
    public string Username { get; set; } = null!;
}
