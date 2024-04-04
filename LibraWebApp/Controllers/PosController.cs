using FluentValidation;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common;
using LibraBll.DTOs.Pos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LibraWebApp.Controllers
{
    public class PosController : Controller
    {
        private readonly IPosRepository _posRepository;
        private readonly IValidator<PosPostDTO> _createPosValidator;

        public PosController(IPosRepository posRepository, IValidator<PosPostDTO> createPosValidator)
        {
            _posRepository = posRepository;
            _createPosValidator = createPosValidator;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetPosById(int id)
        {
            var pos = await _posRepository.GetPosByIdAsync(id);
            return View(pos);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPos()
        {
            List<PosGetDTO> allPos = await _posRepository.GetAllPosAsync();

            if (!allPos.Any())
                return null;

            return PartialView(allPos);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllPosJson()
        {
            List<PosGetDTO> allPos = await _posRepository.GetAllPosAsync();

            if (!allPos.Any())
                return Json(new { }, JsonRequestBehavior.AllowGet);

            return Json(allPos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddPos()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddPos(PosPostDTO pos)
        {
            var results = _createPosValidator.Validate(pos);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return PartialView();
            }

            // var holidayDays = pos.DaysClosed.Split(',').ToList();
            pos.DaysClosed = new List<string>();

            List<string> allDays = Request.Form["dayOfWeek"].ToString().Split(',').ToList();

            pos.DaysClosed = allDays.Where(d => d != "false").ToList();

            //if (Request.Form["dayOfWeek"].Count() > 0)
            //{
            //	foreach (var day in Request.Form["dayOfWeek"])
            //	{
            //		pos.DaysClosed.Add(day.ToString());
            //		//pos.DaysClosed.Add(day.);
            //	}

            //}

            //pos.DaysClosed = Request.Form["DaysClosed"].ToString().Split(',').ToList();

            await _posRepository.AddPosAsync(pos);
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePos(PosPostDTO pos)
        {
            _posRepository.UpdatePos(pos);
            return PartialView();
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePos(int id)
        {
            _posRepository.DeletePos(id);
            return null;
        }

        public ActionResult GetPosByLocation()
        {
            return View();
        }

        public ActionResult GetPosByStatus()
        {
            return View();
        }

        public ActionResult GetPosByType()
        {
            return View();
        }
    }
}