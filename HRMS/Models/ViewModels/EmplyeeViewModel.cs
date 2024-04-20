using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models.ViewModels
{
	public class EmplyeeViewModel
	{
        public string Id { get; set; }  
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

        //[ForeignKey(nameof(DapartmentId))] for list data binding
        public string DepartmentId { get; set; }

        //[ForeignKey(nameof(PositionId))] for list data binding
        public string PositionId { get; set; }

        public string DepartmentInfo { get; set; }

        public string PositionInfo { get; set; }

    }
}   

