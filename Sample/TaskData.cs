namespace PlannerToGantt
{
    public class TaskData
    {
        public int TaskId { get; set; } = 0;
        public string Id { get; set; } = "";
        public string TaskName { get; set; } = "";
        public string TaskType { get; set; } = "FixedDuration";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Duration { get; set; } = "0";
        public int Progress { get; set; } = 0;
        public int? ParentId { get; set; } = null;
        public string AssignedTo { get; set; } = "";
        public List<ResourceAllocateData>? Resources { get; set; }

        public string? Priority { get; set; }
        public string? Status { get; set; }
        public string? BucketName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}