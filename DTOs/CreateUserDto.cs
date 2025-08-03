using System.ComponentModel.DataAnnotations;

namespace _net_taskApiSimple.DTOs;

public class CreateUserDto
{
    [Required]
    public string Username { get; set; } = null!;
}
