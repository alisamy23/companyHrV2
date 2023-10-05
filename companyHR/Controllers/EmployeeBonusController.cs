using AutoMapper;
using company.BL.Model;
using company.DAL.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryUsingEFinMVC.GenericRepository;

namespace companyHR.Controllers
{
    public class EmployeeBonusController : Controller
    {
        private IGenericRepository<employeesBonu> employeeBounsRep;

        private readonly IMapper _mapper;
        public EmployeeBonusController(IGenericRepository<employeesBonu> _employeeBounsRep, IMapper mapper)
        {

            this.employeeBounsRep = _employeeBounsRep;
            this._mapper = mapper;
        }
        public IActionResult Index()
        {
            var data = employeeBounsRep.GetAll(p=>p.bonus,page=>page.employee);
            var model = _mapper.Map<IEnumerable<EmployeeBounsVM>>(data);
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeBounsVM model)
        {
            try
            {
                ModelState.Remove("bonus");
                ModelState.Remove("employee");
                if (ModelState.IsValid)
                {
                    var data = _mapper.Map<employeesBonu>(model);
                    employeeBounsRep.Insert(data);

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
            var data = employeeBounsRep.GetById(id, p => p.bonus, page => page.employee);
            var model = _mapper.Map<EmployeeBounsVM>(data);
            return View(model);
        }
        [HttpGet]
        public IActionResult edit(int id)
        {
            var data = employeeBounsRep.GetById(id);
            var model = _mapper.Map<EmployeeBounsVM>(data);

            return View(model);
        }
        [HttpPost]
        public IActionResult edit(EmployeeBounsVM model)
        {
            try
            {
                return editEmployeeBouns(model);
            }
            catch
            {
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult delete(int id)
        {
            var data = employeeBounsRep.GetById(id, p => p.bonus, page => page.employee);
            var model = _mapper.Map<EmployeeBounsVM>(data);

            return View(model);
        }
        [HttpPost]
        public IActionResult delete(EmployeeBounsVM model)
        {

            try
            {
                var data = _mapper.Map<employeesBonu>(model);
                employeeBounsRep.Delete(data.id);

                return RedirectToAction("Index");


            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Not Deleted");
                return View(model);

            }
        }




        private IActionResult editEmployeeBouns(EmployeeBounsVM model)
        {
            ModelState.Remove("bonus");
            ModelState.Remove("employee");
            if (ModelState.IsValid)
            {

                var data = _mapper.Map<employeesBonu>(model);
                employeeBounsRep.Update(data);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

    }
}
