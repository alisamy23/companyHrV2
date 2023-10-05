using AutoMapper;
using company.BL.Model;
using company.DAL.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryUsingEFinMVC.GenericRepository;

namespace companyHR.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;
        private IGenericRepository<Department> departmentRep;
        public DepartmentController(IGenericRepository<Department> _departmentRep, IMapper mapper)
        {
            this.departmentRep = _departmentRep;
            this._mapper = mapper;
        }
        public IActionResult Index()
        {
            var data= departmentRep.GetAll();
            var model = _mapper.Map<IEnumerable<DepartmentVM>>(data);
            return View(model);
        }
        [HttpGet]
        public IActionResult Create() 
        { 
        
          return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentVM  model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _mapper.Map<Department>(model);
                    departmentRep.Insert(data);

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
         var data= departmentRep.GetById(id);
            var model = _mapper.Map<DepartmentVM>(data);
          return View(model);
        }        
        [HttpGet]
        public IActionResult edit(int id)
        {
            var data = departmentRep.GetById(id);
            var model = _mapper.Map<DepartmentVM>(data);

            return View(model);
        }

        [HttpPost]
        public IActionResult edit(DepartmentVM model)
        {
            try
            {
                return editDepartment(model);
            }
            catch
            {
                return View(model);
            }

        }
        [HttpGet]
        public IActionResult delete(int id) 
        {
            var data = departmentRep.GetById(id);
            var model = _mapper.Map<DepartmentVM>(data);

            return View(model);
        }    
        [HttpPost]
        public IActionResult delete(DepartmentVM model) 
        {

            try
            {
                var data = _mapper.Map<Department>(model);
                    departmentRep.Delete(data.Id);

                    return RedirectToAction("Index");
               

            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Not Deleted");

                return View(model);

            }
        }
        private IActionResult editDepartment(DepartmentVM model)
        {
            if (ModelState.IsValid)
            {
                var data = _mapper.Map<Department>(model);
                departmentRep.Update(data);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public JsonResult getAllDepartment()
        {
            var data = departmentRep.GetAll().Select(o => new { o.Id, o.Name }).ToList();

            return Json(data);
        }
    }
}
