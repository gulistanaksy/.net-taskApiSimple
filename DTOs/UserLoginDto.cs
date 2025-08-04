using System.ComponentModel.DataAnnotations;

namespace _net_taskApiSimple.DTOs;

public class UserLoginDto
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
