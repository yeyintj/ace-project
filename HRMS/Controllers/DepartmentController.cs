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
    public class DepartmentController : Controller
    {
        // GET: /<controller>/

        private readonly HRMSDbContext _dbContext;

        public DepartmentController(HRMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IActionResult Entry()
        {
            var position = _dbContext.Positions.Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code
            }).OrderBy(o => o.Code).ToList();
            ViewBag.Positions = position;

            return View();
        }

        [HttpPost]
        public IActionResult Entry(DepartmentViewModel departmentViewModel)
        {

            try
            {
                DepartmentEntity departmentEntity = new DepartmentEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = departmentViewModel.Code,
                    Name = departmentViewModel.Name,
                    ExtensionPhone = departmentViewModel.ExtensionPhone,
                    PositionId = departmentViewModel.PositionId,
                    CreatedAt = DateTime.UtcNow,
                    Ip = Dns.GetHostEntry(Dns.GetHostName())
                                            .AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                                            .ToString(),
                    IsInActive = true,
                };

                _dbContext.Departments.Add(departmentEntity);
                _dbContext.SaveChanges();

                TempData["info"] = "Save successfully data to the system";
            }catch(Exception e)
            {
                TempData["info"] = "Error while saving data to the system";
            }
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            IList<DepartmentViewModel> departmentList = _dbContext.Departments.Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
                ExtensionPhone = s.ExtensionPhone,
            }).ToList();


            return View(departmentList);
        }

        public IActionResult Delete(string Id)
        {
            try
            {
                DepartmentEntity department = _dbContext.Departments.Where(w => w.Id == Id).FirstOrDefault();

                if(department is not null)
                {
                    _dbContext.Departments.Remove(department);
                    _dbContext.SaveChanges();
                }

                TempData["info"] = "Delete Successfully!";
            }catch(Exception e)
            {
                TempData["info"] = "Error while deleting data from the system";
            }

            return RedirectToAction("List");
        }

        public IActionResult Edit(string Id)
        {
            DepartmentViewModel department = _dbContext.Departments.Where(w => w.Id == Id).Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
                ExtensionPhone = s.ExtensionPhone,
            }).FirstOrDefault();

            return View(department);
        }
    }
}

