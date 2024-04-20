using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models.ViewModels
{
	public class DailyAttendanceViewModel
	{
        public string Id { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }

        //[ForeignKey(nameof(EmplyeeId))] for list data binding
        public string EmplyeeId { get; set; }

        //[ForeignKey(nameof(DepartmentId))] for list data binding
        public string DepartmentId { get; set; }
    }
}

