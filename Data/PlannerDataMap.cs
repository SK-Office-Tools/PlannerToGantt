using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using PlannerToGantt;

namespace PlannerToGantt.Data
{
	public class ListConverter : DefaultTypeConverter
	{
		public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
		{
			if (string.IsNullOrEmpty(text))
				return new List<string>();
			return text.Split(';').ToList();
		}

		public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
		{
			if (value == null)
				return String.Empty;
			return String.Join(';', value);
		}
	}

	public class PlannerDataMap : ClassMap<TaskData>
	{
		public PlannerDataMap()
        {
			var ShortDateFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            _ = new PlannerDataMap(ShortDateFormat);
		}
		public PlannerDataMap(string dateFormat)
		{
			Map(m => m.StartDate).Name("Start Date").TypeConverterOption.Format(dateFormat);  //	Created Date
			Map(m => m.EndDate).Name("Due Date").TypeConverterOption.Format(dateFormat);  // Completed Date
			Map(m => m.Id).Name("Task ID");
			Map(m => m.TaskName).Name("Task Name");
			Map(m => m.AssignedTo).Name("Assigned To");
			Map(m => m.Priority).Name("Priority");
			Map(m => m.Status).Name("Progress");
			Map(m => m.BucketName).Name("Bucket Name");
			Map(m => m.CreatedDate).Name("Created Date").TypeConverterOption.Format(dateFormat);
			Map(m => m.CompletedDate).Name("Completed Date").TypeConverterOption.Format(dateFormat);
			Map(m => m.Labels).Name("Labels").TypeConverter<ListConverter>();
			Map(m => m.ChecklistItems).Name("Checklist Items").TypeConverter<ListConverter>();
		}
	}

	public class PlannerDataMapUK : PlannerDataMap
	{
		public PlannerDataMapUK() : base("dd/MM/yyyy") {}
			}
	public class PlannerDataMapUS : PlannerDataMap
	{
		public PlannerDataMapUS() : base("MM/dd/yyyy") {}
	}
}

