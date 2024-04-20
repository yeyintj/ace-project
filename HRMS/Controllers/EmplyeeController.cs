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
    public class EmplyeeController : Controller
    {
        // GET: /<controller>/

        private readonly HRMSDbContext _dbContext;

        public EmplyeeController(HRMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IActionResult Entry()
        {
            var department = _dbContext.Departments.Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code,

            }).OrderBy(o => o.Code).ToList();
            ViewBag.Departments = department;

            var positions = _dbContext.Positions.Select(s => new PositionViewModel
            {
                Id = s.Id,
                Code = s.Code,

            }).OrderBy(o => o.Code).ToList();

            ViewBag.Positions = positions;
            return View();
        }

        [HttpPost]
        public IActionResult Entry(EmplyeeViewModel emplyeeViewModel)
        {
            try
            {

                EmplyeeEntity emplyee = new EmplyeeEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Ip = Dns.GetHostEntry(Dns.GetHostName())
                                            .AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                                            .ToString(),
                    Code = emplyeeViewModel.Code,
                    Name = emplyeeViewModel.Name,
                    Email = emplyeeViewModel.Email,
                    Gender = emplyeeViewModel.Gender,
                    DOB = emplyeeViewModel.DOB,
                    DOE = emplyeeViewModel.DOE,
                    DOR = emplyeeViewModel.DOR,
                    Address = emplyeeViewModel.Address,
                    BasicSalary = emplyeeViewModel.BasicSalary,
                    Phone = emplyeeViewModel.Phone,
                    DepartmentId = emplyeeViewModel.DepartmentId,
                    PositionId = emplyeeViewModel.PositionId,
                    
                };
                _dbContext.Emplyees.Add(emplyee);
                _dbContext.SaveChanges();
                TempData["info"] = "save successfully the record to the system";
            } catch(Exception e)
            {
                TempData["info"] = "error while saving the record to the system" + e;
            }
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            IList<EmplyeeViewModel> emplyees = (from e in _dbContext.Emplyees
                                                join d in _dbContext.Departments
                                                on e.DepartmentId equals d.Id
                                                join p in _dbContext.Positions
                                                on e.PositionId equals p.Id
                                                select new EmplyeeViewModel
                                                {
                                                    Id = e.Id,
                                                    Name = e.Name,
                                                    Email = e.Email,
                                                    DOB = e.DOB,
                                                    BasicSalary = e.BasicSalary,
                                                    Address = e.Address,
                                                    Gender = e.Gender,
                                                    Phone = e.Phone,
                                                    Code = e.Code,
                                                    DepartmentInfo = d.Code+ "/" + d.Name,
                                                    PositionInfo = p.Code + "/" + p.Name,
                                                }).ToList();

            return View(emplyees);
        }

        public IActionResult Edit(string Id)
        {
            EmplyeeViewModel emplyee = _dbContext.Emplyees.Where(x => x.Id == Id).Select(s => new EmplyeeViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                DOB = s.DOB,
                BasicSalary = s.BasicSalary,
                Address = s.Address,
                Gender = s.Gender,
                Phone = s.Phone,
                Code = s.Code,
                DOE = s.DOE,
                DepartmentId = s.DepartmentId,
                PositionId = s.PositionId
            }).SingleOrDefault();

            //for dinding the department select
            var departments = _dbContext.Departments.Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code,
            }).OrderBy(o => o.Code).ToList();
            ViewBag.Department = departments;

            var positions = _dbContext.Positions.Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code,
            }).OrderBy(o => o.Code).ToList();
            ViewBag.Position = positions;
            return View(emplyee);
        }

        [HttpPost]
        public IActionResult Updated(EmplyeeViewModel emplyeeViewModel)
        {
            try
            {

                EmplyeeEntity emplyeeEntity = new EmplyeeEntity()
                {

                    Id = emplyeeViewModel.Id,
                    Code = emplyeeViewModel.Code,
                    Name = emplyeeViewModel.Code,
                    Email = emplyeeViewModel.Email,
                    Phone = emplyeeViewModel.Phone,
                    DOB = emplyeeViewModel.DOB,
                    DOE = emplyeeViewModel.DOE,
                    DOR = emplyeeViewModel.DOR,
                    Address = emplyeeViewModel.Address,
                    BasicSalary = emplyeeViewModel.BasicSalary,
                    Gender = emplyeeViewModel.Gender,
                    ModifiedAt = DateTime.Now,// for updat edit purpose
                    DepartmentId = emplyeeViewModel.DepartmentId,
                    PositionId = emplyeeViewModel.PositionId,

                };

                _dbContext.Emplyees.Update(emplyeeEntity);
                _dbContext.SaveChanges();

                TempData["info"] = "updated successfully!";
            }catch(Exception e)
            {
                TempData["info"] = "errror when updated system!";
            }

            return RedirectToAction("List");
        }

        public IActionResult Delete(string Id)
        {
            try
            {
                var emplyee = _dbContext.Emplyees.Find(Id);

                if (emplyee is not null)
                {
                    _dbContext.Emplyees.Remove(emplyee);
                    _dbContext.SaveChanges();

                    TempData["info"] = "delete successfully!";
                }

            } catch (Exception e)
            {
                TempData["info"] = "delete successfully!";
            }

            return RedirectToAction("List");
        }

    }
}

