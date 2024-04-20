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

namespace BasicInfo.Controllers
{
    public class PositionController : Controller
    {
        // GET: /<controller>/

        private readonly HRMSDbContext _dbContext;

        public PositionController(HRMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IActionResult Entry()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Entry(PositionViewModel PositionViewModel)
        {
            try
            {
                PositionEntity position = new PositionEntity(){
                    Id = Guid.NewGuid().ToString(),//generate 36 char for primary key
                    CreatedAt = DateTime.UtcNow,
                    //ModifiedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Ip = Dns.GetHostEntry(Dns.GetHostName())
                                        .AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                                        .ToString(),
                    Code = PositionViewModel.Code,
                    Name = PositionViewModel.Name,
                    Level = PositionViewModel.Level,
                };

                _dbContext.Positions.Add(position);
                _dbContext.SaveChanges();

                TempData["info"] = "save successfully the record to the system";
            }
            catch(Exception e)
            {
                TempData["info"] =  e.Message;
            }

            return RedirectToAction("List");
        }

        public IActionResult List()
        {

            IList<PositionViewModel> positionList = _dbContext.Positions.Select(s => new PositionViewModel()
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
                Level = s.Level,
            }).OrderBy(o => o.Name).ToList();

            return View(positionList);
        }


        public IActionResult Delete(string Id)
        {

            try
            {
                PositionEntity position = _dbContext.Positions.Where(w => w.Id == Id).FirstOrDefault();

                if (position is not null)
                {
                    _dbContext.Positions.Remove(position);
                    _dbContext.SaveChanges();

                    TempData["info"] = "delete successfully!";
                }

            }
            catch (Exception e)
            {

                TempData["info"] = "error while deleting data from the system";

            }

            return RedirectToAction("List");
        }

        public IActionResult Edit(string Id)
        {

            PositionViewModel editPosition = _dbContext.Positions.Where(w => w.Id == Id).Select(s => new PositionViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
                Level = s.Level,
            }).FirstOrDefault();

            return View(editPosition);
        }

        [HttpPost]
        public IActionResult Update(PositionViewModel positionViewModel)
        {
            try
            {

                PositionEntity updatePosition = new PositionEntity()
                {
                    Id = positionViewModel.Id,
                    Code = positionViewModel.Code,
                    Name = positionViewModel.Name,
                    Level = positionViewModel.Level,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    IsInActive = true,
                    Ip = Dns.GetHostEntry(Dns.GetHostName())
                                        .AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                                        .ToString(),
                };

                _dbContext.Positions.Update(updatePosition);
                _dbContext.SaveChanges();

                TempData["info"] = "updated successfully!";
            }catch(Exception e)
            {
                TempData["info"] = "error when updated the data!";
            }

            return RedirectToAction("List");
        }
    }
}

