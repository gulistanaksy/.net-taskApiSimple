using _net_taskApiSimple.DTOs;

namespace _net_taskApiSimple.Interfaces;


//  servis sınıfının hangi metotları içermesi gerektiğini belirliyor.
// Repository'yi kullanarak veri katmanıyla haberleşir ama dışa DTO ile cevap verir.
public interface ITaskService
{
    List<TaskResponseDto> GetTasks();
    TaskResponseDto? GetTaskById(int id);
    TaskResponseDto CreateTask(string title);
    bool UpdateTask(int id, string title, bool isCompleted);
    bool DeleteTask(int id);
}
