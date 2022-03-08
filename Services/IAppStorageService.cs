
namespace PlannerToGantt.Services
{
    public interface IAppStorageService
    {
        Task<IEnumerable<TaskData>> GetTaskList();
        Task SaveTaskList(IEnumerable<TaskData> tasks);
    }
}