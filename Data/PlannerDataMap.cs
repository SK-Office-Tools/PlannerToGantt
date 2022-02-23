using System;
using CsvHelper.Configuration;
using PlannerToGantt;

namespace PlannerToGantt.Data
{
	public class PlannerDataMap : ClassMap<TaskData>
	{
		public PlannerDataMap()
		{
			Map(m => m.StartDate).Name("Start Date").TypeConverterOption.Format("dd/MM/yyyy").Default(DateTime.MinValue);  //	Created Date
			Map(m => m.EndDate).Name("Due Date").TypeConverterOption.Format("dd/MM/yyyy").Default(DateTime.MaxValue);  // Completed Date
			Map(m => m.TaskId).Name("Task ID");
			Map(m => m.TaskName).Name("Task Name");
			Map(m => m.AssignedTo).Name("Assigned To");//	Priority, Progress, Bucket Name
		}
	}
}

