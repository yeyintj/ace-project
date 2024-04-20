using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Models.ViewModels
{
	public class PositionViewModel
	{
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}

