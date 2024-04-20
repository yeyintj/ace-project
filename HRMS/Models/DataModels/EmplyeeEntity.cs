using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models.DataModels
{
	[Table("Emplyee")]
	public class EmplyeeEntity : BaseEntity
	{

		public string Code { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Gender { get; set; }
		public DateTime DOB { get; set; }
		public DateTime DOE { get; set; }
		public DateTime? DOR { get; set; }
		public string? Address { get; set; }
		public decimal BasicSalary { get; set; }
		public int? Phone { get; set; }

		[ForeignKey(nameof(DepartmentId))]
		public string DepartmentId { get; set; }

        [ForeignKey(nameof(PositionId))]
        public string PositionId { get; set; }




    }
}

