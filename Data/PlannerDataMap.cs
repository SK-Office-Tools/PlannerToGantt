using System;
using CsvHelper.Configuration;
using PlannerToGantt;

namespace PlannerToGantt.Data
{
	public class PlannerDataMap : ClassMap<TaskData>
	{
		public PlannerDataMap()
		{
			Map(m => m.StartDate).Name("Start Date").TypeConverterOption.Format("dd/MM/yyyy");  //	Created Date
			Map(m => m.EndDate).Name("Due Date").TypeConverterOption.Format("dd/MM/yyyy");  // Completed Date
			Map(m => m.Id).Name("Task ID");
			Map(m => m.TaskName).Name("Task Name");
			Map(m => m.AssignedTo).Name("Assigned To");
			Map(m => m.Priority).Name("Priority");
			Map(m => m.Status).Name("Progress");
			Map(m => m.BucketName).Name("Bucket Name");
			Map(m => m.CreatedDate).Name("Created Date").TypeConverterOption.Format("dd/MM/yyyy");
			Map(m => m.CompletedDate).Name("Completed Date").TypeConverterOption.Format("dd/MM/yyyy");
		}
	}
}

