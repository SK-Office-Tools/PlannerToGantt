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
		public string ShortDateFormat { get; set; } = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
		public PlannerDataMap()
		{
			if (string.IsNullOrEmpty(ShortDateFormat))
				ShortDateFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

			Map(m => m.StartDate).Name("Start Date").TypeConverterOption.Format(ShortDateFormat);  //	Created Date
			Map(m => m.EndDate).Name("Due Date").TypeConverterOption.Format(ShortDateFormat);  // Completed Date
			Map(m => m.Id).Name("Task ID");
			Map(m => m.TaskName).Name("Task Name");
			Map(m => m.AssignedTo).Name("Assigned To");
			Map(m => m.Priority).Name("Priority");
			Map(m => m.Status).Name("Progress");
			Map(m => m.BucketName).Name("Bucket Name");
			Map(m => m.CreatedDate).Name("Created Date").TypeConverterOption.Format(ShortDateFormat);
			Map(m => m.CompletedDate).Name("Completed Date").TypeConverterOption.Format(ShortDateFormat);
			Map(m => m.Labels).Name("Labels").TypeConverter<ListConverter>();
			Map(m => m.ChecklistItems).Name("Checklist Items").TypeConverter<ListConverter>();
		}
	}

	public class PlannerDataMapUK : PlannerDataMap
	{
		public PlannerDataMapUK() : base() { ShortDateFormat = "dd/MM/yyyy"; }
			}
}

