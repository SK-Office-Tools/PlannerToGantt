namespace PlannerToGantt
{
    public class TaskData
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Duration { get; set; }
        public int Progress { get; set; }
        public int? ParentId { get; set; }
        public double? Work { get; set; }
        public List<ResourceAllocateData>? Resources { get; set; }
    }
}