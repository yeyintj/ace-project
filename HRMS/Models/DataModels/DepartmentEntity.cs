using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Models.DataModels
{
	[Table("Department")]
	public class DepartmentEntity : BaseEntity
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public string ExtensionPhone { get; set; }

		[ForeignKey(nameof(PositionId))]
		public string PositionId { get; set; }


	}
}
	
