using System.ComponentModel.DataAnnotations;

namespace _net_taskApiSimple.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string PasswordHash { get; set; } = null!;


    // navigation property
    // = new(); ifadesi, bu listenin boş liste ile başlatıldığını gösterir (null değil).
    public List<TaskItem> Tasks { get; set; } = new();
}
