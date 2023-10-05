using AutoMapper;
using company.BL.Model;
using company.DAL.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryUsingEFinMVC.GenericRepository;

namespace companyHR.Controllers
{
    public class VacationController : Controller
    {
        private IGenericRepository<vacation> vacationRep;

        private readonly IMapper _mapper;
        public VacationController(IGenericRepository<vacation> _vacationRep, IMapper mapper)
        {

            this.vacationRep = _vacationRep;
            this._mapper = mapper;
        }
        public IActionResult Index()
        {
            var data = vacationRep.GetAll();
            var model = _mapper.Map<IEnumerable<VacationVM>>(data);
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(VacationVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _mapper.Map<vacation>(model);
                    vacationRep.Insert(data);

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
            var data = vacationRep.GetById(id);
            var model = _mapper.Map<VacationVM>(data);
            return View(model);
        }
        [HttpGet]
        public IActionResult edit(int id)
        {
            var data = vacationRep.GetById(id);
            var model = _mapper.Map<VacationVM>(data);

            return View(model);
        }

        [HttpPost]
        public IActionResult edit(VacationVM model)
        {
            try
            {
                return editVacation(model);
            }
            catch
            {
                return View(model);
            }

        }



        [HttpGet]
        public IActionResult delete(int id)
        {
            var data = vacationRep.GetById(id);
            var model = _mapper.Map<VacationVM>(data);

            return View(model);
        }
        [HttpPost]
        public IActionResult delete(VacationVM model)
        {

            try
            {
                var data = _mapper.Map<vacation>(model);
                vacationRep.Delete(data.id);

                return RedirectToAction("Index");


            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Not Deleted");
                return View(model);

            }
        }




        private IActionResult editVacation(VacationVM model)
        {
            if (ModelState.IsValid)
            {

                var data = _mapper.Map<vacation>(model);
                vacationRep.Update(data);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult getAllVacation()
        {
            var data = vacationRep.GetAll().Select(o => new { o.id, o.vacationName }).ToList();

            return Json(data);
        }
    }
}
