using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Excel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PlannerToGantt.Data;
using System.Globalization;

namespace PlannerToGantt.Pages
{
    public partial class Index
    {
        ElementReference dropZoneElement;
        ElementReference inputFileContainer;
        IJSObjectReference _module;
        IJSObjectReference _dropZoneInstance;
        List<TaskData> _tasks = null;
        private bool _isLoading = false;
        private string SpinnerText = "";
        private DateTime ProjectStart = DateTime.Today.AddMonths(-1);
        private DateTime ProjectEnd = DateTime.Today.AddMonths(3);
        private List<ResourceAllocateData> ResourceCollection { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _tasks = (List<TaskData>)await AppStorageService.GetTaskList();
            if (_tasks != null)
            {
                _isLoading = true;
                await ProcessTasks();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./dropZone.js");
                _dropZoneInstance = await _module.InvokeAsync<IJSObjectReference>("initializeFileDropZone", dropZoneElement, inputFileContainer);
            }
        }

        private int WorkingDaysBetween(DateTime startDate, DateTime endDate)
        {
            if (startDate == endDate)
                return 0;
            var duration = (endDate - startDate);
            var days = 0;
            for (var dt = startDate; dt <= endDate; dt = dt.AddDays(1))
            {
                if (dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday)
                    days++;
            }

            return days;
        }

        private DateTime? EarliestCreatedTask => _tasks.Where(x => x.CreatedDate != null).OrderBy(t => t.CreatedDate).First().CreatedDate;
        private DateTime? EarliestTask => _tasks.Where(x => x.StartDate != null).OrderBy(t => t.StartDate).First().StartDate;
        private DateTime? LatestTask => _tasks.Where(x => x.EndDate != null).OrderByDescending(t => t.EndDate).First().EndDate;
        private DateTime? LatestCompletedTask => _tasks.Where(x => x.CompletedDate != null).OrderByDescending(t => t.CompletedDate).First().CompletedDate;
        async Task ProcessTasks()
        {
            SpinnerText = "Processing Tasks...";
            _tasks = _tasks.Where(t => t.BucketName != "TBD" && t.BucketName != "Backlog" && t.Status != "Completed").ToList(); // Remove TBD and Backlog Items
            //var earliestCreated = (EarliestCreatedTask ?? ProjectStart).AddDays(-7);
            ProjectStart = (EarliestTask ?? ProjectStart).AddDays(-7);
            ProjectEnd = (LatestTask ?? ProjectEnd).AddMonths(1);
            //if (earliestCreated < ProjectStart) ProjectStart = earliestCreated;
            //var altProjectCompleted = LatestCompletedTask;
            var resources = _tasks.Select(t => t.AssignedTo);
            var tmp = new List<string>();
            foreach (var res in resources)
                tmp.AddRange(res.Split(';'));
            tmp = tmp.Distinct().ToList();
            int resId = 0;
            int taskId = 0;
            ResourceCollection = tmp.Select(res => new ResourceAllocateData{ResourceId = resId++, ResourceName = res}).ToList();
            foreach (var task in _tasks)
            {
                task.TaskId = taskId++;
                if (task.StartDate == null)
                    task.StartDate = task.CreatedDate;
                if (task.CompletedDate != null)
                    task.EndDate = task.CompletedDate;
                if (task.EndDate == null)
                    task.EndDate = task.StartDate;
                task.Duration = WorkingDaysBetween(task.StartDate.Value, task.EndDate.Value).ToString(); // (task.EndDate - task.StartDate);
                //task.Duration = duration.Value.TotalDays.ToString();
                task.Resources = task.AssignedTo.Split(";").Select(x => ResourceCollection.First(y => y.ResourceName == x)).ToList();
            }

            SpinnerText = "";
            _isLoading = false;
        }

        async Task<List<TaskData>> ReadFromExcel(IBrowserFile importFile, bool forceUK = false)
        {
            await using MemoryStream ms = new();
            await importFile.OpenReadStream().CopyToAsync(ms);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {HasHeaderRecord = true};
            try
            {
                using var parser = new ExcelParser(ms, "Tasks", config);
                using (var csvReader = new CsvReader(parser))
                {
                    if (forceUK)
                        csvReader.Context.RegisterClassMap<PlannerDataMapUK>();
                    else
                        csvReader.Context.RegisterClassMap<PlannerDataMap>();
                    await csvReader.ReadAsync();
                    csvReader.ReadHeader();
                    return await csvReader.GetRecordsAsync<TaskData>().ToListAsync();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        async Task OnChange(InputFileChangeEventArgs e)
        {
            _isLoading = true;
            StateHasChanged();
            var importFile = e.File;
            _tasks = await ReadFromExcel(importFile);
            if (_tasks == null)
            {
                _tasks = await ReadFromExcel(importFile, true);
            }

            if (_tasks == null)
            {
                //  Output a message
                Console.WriteLine("NULL task list");
            }
            else
            {
                await AppStorageService.SaveTaskList(_tasks);
                await ProcessTasks();
            }

            StateHasChanged();
        }

        public async ValueTask DisposeAsync()
        {
            if (_dropZoneInstance != null)
            {
                await _dropZoneInstance.InvokeVoidAsync("dispose");
                await _dropZoneInstance.DisposeAsync();
            }

            if (_module != null)
            {
                await _module.DisposeAsync();
            }
        }
    }
}