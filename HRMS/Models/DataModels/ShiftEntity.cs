using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models.DataModels
{
	[Table("Shift")]
	public class ShiftEntity : BaseEntity
	{
		public string Name { get; set; }
		public TimeSpan InTime { get; set; }
		public TimeSpan OutTime { get; set; }
		public TimeSpan LateAfter { get; set; }
		public TimeSpan EarlyOutBefore { get; set; }

		[ForeignKey(nameof(AttendancePoliceId))]
		public string AttendancePoliceId { get; set; }
	}
}

