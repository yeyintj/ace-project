using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models.ViewModels
{
	public class DepartmentViewModel
	{
        public string Id { get; set; }  
        public string Code { get; set; }
        public string Name { get; set; }
        public string ExtensionPhone { get; set; }

        //[ForeignKey(nameof(PositionId))]
        public string PositionId { get; set; }
    }
}

