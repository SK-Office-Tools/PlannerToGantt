using System;
using Blazored.LocalStorage;

namespace PlannerToGantt.Services
{
    public class AppStorageService : IAppStorageService
    {
        private readonly ILocalStorageService StorageService;
        private const string TASK_DATA = "TaskData";

        public AppStorageService(ILocalStorageService storageService)
        {
            StorageService = storageService;
        }

        public async Task SaveTaskList(IEnumerable<TaskData> tasks)
        {
            await StorageService.SetItemAsync(TASK_DATA, tasks);
        }

        public async Task<IEnumerable<TaskData>> GetTaskList()
        {
            return await StorageService.GetItemAsync<IEnumerable<TaskData>>(TASK_DATA);
        }
    }
}

