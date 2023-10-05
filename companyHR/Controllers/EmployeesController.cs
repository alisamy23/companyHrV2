using AutoMapper;
using company.BL.Model;
using company.DAL.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryUsingEFinMVC.GenericRepository;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace companyHR.Controllers
{
    public class EmployeesController : Controller
    {

        private readonly IMapper _mapper;
        private IGenericRepository<employee> employeeRep;
        public EmployeesController(IGenericRepository<employee> _employeeRep, IMapper mapper)
        {

            this.employeeRep = _employeeRep;
            this._mapper = mapper;
        }
        public IActionResult Index()
        {
            var data = GetData();
            var model = _mapper.Map<IEnumerable<EmployeeVM>>(data);
            return View(model);
        }

        private IEnumerable<employee> GetData()
        {
            return employeeRep.GetAll(p => p.deprtment, p => p.gender, P => P.religion,p=>p.job);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeVM model)
        {
            try
            {
                removeValidationFromModel();
                if (ModelState.IsValid)
                {
                    var data = _mapper.Map<employee>(model);
                    employeeRep.Insert(data);

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View(model);

            }

        }
        [HttpGet]
        public IActionResult details(int id)
        {
            var data = employeeRep.GetById(id,P=>P.job,P=>P.gender,P => P.religion);
            var model = _mapper.Map<EmployeeVM>(data);
            return View(model);
        }
        [HttpGet]
        public IActionResult edit(int id)
        {
            var data = employeeRep.GetById(id);
            var model = _mapper.Map<EmployeeVM>(data);

            return View(model);
        }

        [HttpPost]
        public IActionResult edit(EmployeeVM model)
        {
            try
            {
                return editEmployee(model);
            }
            catch 
            {
                return View(model);
            }

        }
        [HttpGet]
        public IActionResult delete(int id)
        {
            var data = employeeRep.GetById(id, P => P.job, P => P.gender, P => P.religion);
            var model = _mapper.Map<EmployeeVM>(data);

            return View(model);
        }
        [HttpPost]
        public IActionResult delete(EmployeeVM model)
        {

            try
            {
                var data = _mapper.Map<employee>(model);
                employeeRep.Delete(data.id);

                return RedirectToAction("Index");


            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Not Deleted");
                return View(model);

            }
        }
        private IActionResult editEmployee( EmployeeVM model)
        {
            removeValidationFromModel();

            if (ModelState.IsValid)
            {
                var data = _mapper.Map<employee>(model);
                employeeRep.Update(data);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }

        }
        void removeValidationFromModel()
        {
            ModelState.Remove("deprtment");
            ModelState.Remove("religion");
            ModelState.Remove("gender");
        }
        [HttpPost]
        public JsonResult getAllEmployee()
        {
            var data = employeeRep.GetAll().Select(o => new {o.id,o.employeeName}).ToList();

            return Json(data);
        }


    

    }
}
