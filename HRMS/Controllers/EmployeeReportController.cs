using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.DAO;
using HRMS.Models.ViewModels;
using HRMS.Reports;
using HRMS.Utilies;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRMS.Controllers
{
    public class EmployeeReportController : Controller
    {
        // GET: /<controller>/

        private readonly IEmployeeReport _employeeReport;
        public EmployeeReportController(IEmployeeReport employeeReport)
        {
            _employeeReport = employeeReport;
        }


        public IActionResult EmployeeDetailReport() => View();

        public IActionResult ReportByEmployeeFromCodeToCode(string fromCode, string toCode)
        {
            string reportName = $"EmployeeDetail_{Guid.NewGuid():N}.xlsx";

            IList<EmployeeDetailReportViewModel> employees = _employeeReport.EmployeeDetailReport(fromCode, toCode);



            if (employees.Any())
            {
                var exportData = FilesIOHelper.ExporttoExcel<EmployeeDetailReportViewModel>(employees, "employeeDetailReport");
                return File(exportData, "application/vnd.openxmlformats-officedocument.spreedsheetml.sheet", reportName);
            }
            else
            {
                TempData["info"] = "There no data when report to excel";
                return View();
            }
        }
    }
}

