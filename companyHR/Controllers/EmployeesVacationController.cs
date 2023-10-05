using AutoMapper;
using company.BL.Model;
using company.DAL.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryUsingEFinMVC.GenericRepository;

namespace companyHR.Controllers
{
    public class EmployeesVacationController : Controller
    {
        private IGenericRepository<employeesVacation> employeesVacationRep;

        private readonly IMapper _mapper;
        public EmployeesVacationController(IGenericRepository<employeesVacation> _EmployeesVacation, IMapper mapper)
        {

            this.employeesVacationRep = _EmployeesVacation;
            this._mapper = mapper;
        }
        public IActionResult Index()
        {
            var data = employeesVacationRep.GetAll(p => p.vacation, p => p.employee);
            var model = _mapper.Map<IEnumerable<EmployeesVacationVM>>(data);
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]

        public IActionResult Create(EmployeesVacationVM model)
        {
            try
            {
                ModelState.Remove("vacation");
                ModelState.Remove("employee");
                if (ModelState.IsValid)
                {
                    var data = _mapper.Map<employeesVacation>(model);
                    employeesVacationRep.Insert(data);

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
            var data = employeesVacationRep.GetById(id, p => p.vacation, p => p.employee);
            var model = _mapper.Map<EmployeesVacationVM>(data);
            return View(model);
        }
        [HttpGet]
        public IActionResult edit(int id)
        {
            var data = employeesVacationRep.GetById(id);
            var model = _mapper.Map<EmployeesVacationVM>(data);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult edit(EmployeesVacationVM model)
        {
            try
            {
                return editEmployeesVacation(model);
            }
            catch
            {
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult delete(int id)
        {
            var data = employeesVacationRep.GetById(id, p => p.vacation, p => p.employee);
            var model = _mapper.Map<EmployeesVacationVM>(data);

            return View(model);
        }
        [HttpPost]
        public IActionResult delete(EmployeesVacationVM model)
        {

            try
            {
                var data = _mapper.Map<employeesVacation>(model);
                employeesVacationRep.Delete(data);

                return RedirectToAction("Index");


            }
            catch
            {
                return View();

            }
        }




        private IActionResult editEmployeesVacation(EmployeesVacationVM model)
        {
            ModelState.Remove("vacation");
            ModelState.Remove("employee");
            if (ModelState.IsValid)
            {

                var data = _mapper.Map<employeesVacation>(model);
                employeesVacationRep.Update(data);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
    }
}
