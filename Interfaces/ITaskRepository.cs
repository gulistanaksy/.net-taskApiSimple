using _net_taskApiSimple.Models;

namespace _net_taskApiSimple.Interfaces;

// Hiçbir işlem yapılmaz — sadece hangi metotların var olduğunu söyler.
// Arayüz, bir sınıfın hangi metodları mutlaka uygulaması gerektiğini belirtir.

public interface ITaskRepository
{
    List<TaskItem> GetAll();

    //Bulursa TaskItem, bulamazsa null dönebilir (bu yüzden TaskItem? nullable).
    TaskItem? GetById(int id);
    TaskItem Add(string title);
    bool Update(int id, string title, bool isCompleted);
    bool Delete(int id);
}
