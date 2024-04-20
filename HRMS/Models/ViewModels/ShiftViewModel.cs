using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models.ViewModels
{
	public class ShiftViewModel
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }
        public TimeSpan LateAfter { get; set; }
        public TimeSpan EarlyOutBefore { get; set; }

        //[ForeignKey(nameof(AttendancePoliceId))]
        public string AttendancePoliceId { get; set; }
    }
}

