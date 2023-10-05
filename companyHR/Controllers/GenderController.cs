using AutoMapper;
using company.DAL.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryUsingEFinMVC.GenericRepository;

namespace companyHR.Controllers
{
    public class GenderController : Controller
    {
        private readonly IMapper _mapper;
        private IGenericRepository<gender> gender;
        public GenderController(IGenericRepository<gender> _gender, IMapper mapper)
        {

            this.gender = _gender;
            this._mapper = mapper;
        }
        [HttpGet]
        public JsonResult getAllgender()
        {
            var data = gender.GetAll().Select(o => new { o.id, o.genderName }).ToList();

            return Json(data);
        }
    }
}
