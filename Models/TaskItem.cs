using System.ComponentModel.DataAnnotations;
namespace _net_taskApiSimple.Models;

public class TaskItem
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public bool IsCompleted { get; set; }

    // Foreign Key
    public int UserId { get; set; }

    // Navigation
    public User? User { get; set; }
}
