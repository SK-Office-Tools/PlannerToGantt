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
			var shortDateFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
			Map(m => m.StartDate).Name("Start Date").TypeConverterOption.Format(shortDateFormat);  //	Created Date
			Map(m => m.EndDate).Name("Due Date").TypeConverterOption.Format(shortDateFormat);  // Completed Date
			Map(m => m.Id).Name("Task ID");
			Map(m => m.TaskName).Name("Task Name");
			Map(m => m.AssignedTo).Name("Assigned To");
			Map(m => m.Priority).Name("Priority");
			Map(m => m.Status).Name("Progress");
			Map(m => m.BucketName).Name("Bucket Name");
			Map(m => m.CreatedDate).Name("Created Date").TypeConverterOption.Format(shortDateFormat);
			Map(m => m.CompletedDate).Name("Completed Date").TypeConverterOption.Format(shortDateFormat);
			Map(m => m.Labels).Name("Labels").TypeConverter<ListConverter>();
			Map(m => m.ChecklistItems).Name("Checklist Items").TypeConverter<ListConverter>();
		}
	}
}

