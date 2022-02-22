using System;
using CsvHelper.Configuration;
using PlannerToGantt;

namespace PlannerToGantt.Data
{
	public class PlannerDataMap : ClassMap<TaskData>
	{
		public PlannerDataMap()
		{
			Map(m => m.StartDate).Name("StartDate");
		}
	}
}

