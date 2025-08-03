using System.ComponentModel.DataAnnotations;

namespace _net_taskApiSimple.DTOs;

public class CreateTaskDto
{
    [Required(ErrorMessage = "Başlık boş olamaz.")]
    public string Title { get; set; } = string.Empty;
    public int UserId { get; set; }  // görev kimin adına oluşturuluyor?
}
