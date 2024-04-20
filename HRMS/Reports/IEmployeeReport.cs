using System;
using HRMS.Models.ViewModels;

namespace HRMS.Reports
{
	public interface IEmployeeReport
	{
		IList<EmployeeDetailReportViewModel> EmployeeDetailReport(string fromCode, string toCode);
	}
}

