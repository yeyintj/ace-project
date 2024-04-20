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
    public class ShiftController : Controller
    {
        // GET: /<controller>/
        private readonly HRMSDbContext _dbContex;
        public ShiftController(HRMSDbContext dbContext)
        {
            _dbContex = dbContext;
        }

        public IActionResult Entry()
        {
            var attedance = _dbContex.DailyAttendances.Select(s => new DailyAttendanceViewModel
            {
                Id = s.Id,

            }).OrderBy(o => o.Id).ToList();
            ViewBag.Attendance = attedance;


            return View();
        }

        [HttpPost]
        public IActionResult Entry(ShiftViewModel shiftViewModel)
        {

            try
            {
                ShiftEntity shiftEntity = new ShiftEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = shiftViewModel.Name,
                    InTime = shiftViewModel.InTime,
                    OutTime = shiftViewModel.OutTime,
                    LateAfter = shiftViewModel.LateAfter,
                    EarlyOutBefore = shiftViewModel.EarlyOutBefore,
                    AttendancePoliceId = shiftViewModel.AttendancePoliceId,
                    CreatedAt = DateTime.UtcNow,
                    Ip = Dns.GetHostEntry(Dns.GetHostName())
                                        .AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                                        .ToString(),
                    IsInActive = true,
                };
                _dbContex.Shifts.Add(shiftEntity);
                _dbContex.SaveChanges();
                TempData["info"] = "save successfully data to the system";

            }catch(Exception e)
            {
                TempData["info"] = "error when saving data to the system" + e;
            }
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            IList<ShiftViewModel> shiftList = _dbContex.Shifts.Select(s => new ShiftViewModel
            {
                Id = s.Id,
                Name = s.Name,
                InTime = s.InTime,
                OutTime = s.OutTime,
                LateAfter = s.LateAfter,
                EarlyOutBefore = s.EarlyOutBefore,
            }).OrderBy(o => o.Name).ToList();

            return View(shiftList);
        }
    }
}

