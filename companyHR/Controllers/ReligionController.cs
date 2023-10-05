using AutoMapper;
using company.DAL.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryUsingEFinMVC.GenericRepository;

namespace companyHR.Controllers
{
    public class ReligionController : Controller
    {
        private readonly IMapper _mapper;
        private IGenericRepository<religion> religion;
        public ReligionController(IGenericRepository<religion> _religion, IMapper mapper)
        {

            this.religion = _religion;
            this._mapper = mapper;
        }
        [HttpGet]
        public JsonResult getAllReligion()
        {
            var data = religion.GetAll().Select(o => new { o.id, o.religionName }).ToList();

            return Json(data);
        }
    }
}
