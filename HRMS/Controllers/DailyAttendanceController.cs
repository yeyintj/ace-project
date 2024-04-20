using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using HRMS.DAO;
using HRMS.Models.DataModels;
using HRMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRMS.Controllers
{
    public class DailyAttendanceController : Controller
    {
        // GET: /<controller>/

        private readonly HRMSDbContext _dbContext;
        public DailyAttendanceController(HRMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Entry()
        {
            var emplyee = _dbContext.Emplyees.Select(s => new EmplyeeViewModel
            {
                Id = s.Id,
                Code = s.Code
            }).OrderBy(o => o.Code).ToList();
            ViewBag.Emplyees = emplyee;

            var department = _dbContext.Departments.Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code
            }).OrderBy(o => o.Code).ToList();
            ViewBag.Departments = department;

            return View();
        }


        [HttpPost]
        public IActionResult Entry(DailyAttendanceViewModel dailyAttendanceViewModel)
        {
            try
            {
                DailyAttendanceEntity dailyAttendance = new DailyAttendanceEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    Ip = Dns.GetHostEntry(Dns.GetHostName())
                                            .AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                                            .ToString(),
                    IsInActive = true,
                    AttendanceDate = dailyAttendanceViewModel.AttendanceDate,
                    InTime = dailyAttendanceViewModel.InTime,
                    OutTime = dailyAttendanceViewModel.OutTime,
                    EmplyeeId = dailyAttendanceViewModel.EmplyeeId,
                    DepartmentId = dailyAttendanceViewModel.DepartmentId,
                };

                _dbContext.DailyAttendances.Add(dailyAttendance);
                _dbContext.SaveChanges();

                TempData["info"] = "save successfully data to the system";
            }catch(Exception e)
            {
                TempData["info"] = "error when saving data to the system";
            }

            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            IList<DailyAttendanceViewModel> attendanceList = _dbContext.DailyAttendances.Select(s => new DailyAttendanceViewModel
            {
                AttendanceDate = s.AttendanceDate,
                InTime = s.InTime,
                OutTime = s.OutTime,

            }).ToList();

            return View(attendanceList);
        }
    }
}

