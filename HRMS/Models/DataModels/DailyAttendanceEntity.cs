using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models.DataModels
{
	[Table("DailyAttendance")]
	public class DailyAttendanceEntity : BaseEntity
	{
		public DateTime AttendanceDate { get; set; }
		public TimeSpan InTime { get; set; }
		public TimeSpan OutTime { get; set; }

		[ForeignKey(nameof(EmplyeeId))]
		public string EmplyeeId { get; set; }

		[ForeignKey(nameof(DepartmentId))]
		public string DepartmentId { get; set; }

	}
}

