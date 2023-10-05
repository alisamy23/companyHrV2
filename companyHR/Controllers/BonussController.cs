using AutoMapper;
using company.BL.Model;
using company.DAL.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using RepositoryUsingEFinMVC.GenericRepository;

namespace companyHR.Controllers
{
    public class BonussController : Controller
    {
        private readonly IMapper _mapper;
        private IGenericRepository<bonuss> bonusRep;
        public BonussController(IGenericRepository<bonuss> _repository, IMapper mapper)
        {
            this.bonusRep = _repository;
            this._mapper = mapper;
        }
        
        public IActionResult Index()
        {
            var data = bonusRep.GetAll();
            var model = _mapper.Map<IEnumerable<BonusVM>>(data);
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(BonusVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _mapper.Map<bonuss>(model);
                    bonusRep.Insert(data);
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
            var data = bonusRep.GetById(id);
            var model = _mapper.Map<BonusVM>(data);
            return View(model);
        }
        [HttpGet]
        public IActionResult edit(int id)
        {
            var data = bonusRep.GetById(id);
            var model = _mapper.Map<BonusVM>(data);

            return View(model);
        }
        [HttpPost]
        public IActionResult edit(BonusVM model)
        {
            try
            {
                return editBonus(model);
            }
            catch
            {
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult delete(int id)
        {
            var data = bonusRep.GetById(id);
            var model = _mapper.Map<BonusVM>(data);

            return View(model);
        }
        [HttpPost]
        public IActionResult delete(BonusVM model)
        {
            try
            {
                var data = _mapper.Map<bonuss>(model);
                bonusRep.Delete(data.id);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Not Deleted");
                return View(model);
            }
        }
        private IActionResult editBonus(BonusVM model)
        {
            if (ModelState.IsValid)
            {
                var data = _mapper.Map<bonuss>(model);
                bonusRep.Update(data);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        public JsonResult getBonusById(int  id)
        {
            var data = bonusRep.GetById(id);
            var model = _mapper.Map<BonusVM>(data);
            return Json(model);
        }
        [HttpPost]
        public JsonResult getAllbonuses()
        {
            var data = bonusRep.GetAll().Select(o => new { o.id, o.bonusName }).ToList();

            return Json(data);
        }
    }
}
