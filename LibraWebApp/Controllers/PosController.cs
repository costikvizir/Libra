using FluentValidation;
using LibraBll.Abstractions.Repositories;
using LibraBll.DTOs.ComplexObjects;
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
        private readonly IIssueRepository _issueRepository;
        private readonly IValidator<PosPostDTO> _createPosValidator;

        public PosController(IPosRepository posRepository, IIssueRepository issueRepository, IValidator<PosPostDTO> createPosValidator)
        {
            _posRepository = posRepository;
            _issueRepository = issueRepository;
            _createPosValidator = createPosValidator;
        }

        //TODO addpos visible for admin only, add issue user

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetPosById(int id)
        {
            PosGetDTO pos = await _posRepository.GetPosByIdAsync(id);

            List<string> noClosingDays = new List<string> { "No Closing Days" };
            List<string> posWeekDays = _posRepository.GetPosClosingDays(id).Select(d => d.Day).ToList();
            pos.DaysClosed = posWeekDays.Count() > 0 ? posWeekDays : noClosingDays;

            PosIssues posIssues = new PosIssues()
            {
                PosGet = pos,
                Issues = await _issueRepository.GetIssuesByPosIdAsync(id)
            };

            return PartialView("~/Views/Pos/PosDetails.cshtml", posIssues);
        }

        //TODO set svg images in the view: Pos/_PosDetails.cshtml and Pos/EditPos.cshtml
        //TODO return PosIssues from repository
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
            var cities = _posRepository.GetCityList();
            var connectionTypes = _posRepository.GetConnectionTypeList();
            ViewBag.Cities = new SelectList(cities, "Id", "CityName");
            ViewBag.ConnectionTypes = new SelectList(connectionTypes, "Id", "ConnectionType");
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

            var cities = _posRepository.GetCityList();
            var connectionTypes = _posRepository.GetConnectionTypeList();
            ViewBag.Cities = new SelectList(cities, "Id", "CityName");
            ViewBag.ConnectionTypes = new SelectList(connectionTypes, "Id", "ConnectionType");

            return PartialView("GetAllPos");
        }

        [HttpGet]
        public async Task<ActionResult> UpdatePos(int id)
        {
            if (id == 0)
                return null;

            var cities = _posRepository.GetCityList();
            var connectionTypes = _posRepository.GetConnectionTypeList();
            var posWeekDays = _posRepository.GetPosClosingDays(id);

            ViewBag.Cities = new SelectList(cities, "Id", "CityName");
            ViewBag.ConnectionTypes = new SelectList(connectionTypes, "Id", "ConnectionType");

            PosGetDTO posGet = await _posRepository.GetPosByIdAsync(id);
            int cityId = _posRepository.GetCityList().FirstOrDefault(c => c.CityName == posGet.City).Id;
            int connectionTypeId = _posRepository.GetConnectionTypeList().FirstOrDefault(c => c.ConnectionType == posGet.ConnectionType).Id;

            PosEditDTO pos = new PosEditDTO
            {
                Id = id,
                Name = posGet.Name,
                Telephone = posGet.Telephone,
                Cellphone = posGet.Cellphone,
                Address = posGet.Address,
                CityId = cityId,
                Model = posGet.Model,
                Brand = posGet.Brand,
                DaysClosed = posWeekDays.Select(d => d.Day).ToList(),
                ConnectionType = connectionTypeId,
                MorningOpening = posGet.MorningProgram.Split('-')[0].Trim(),
                MorningClosing = posGet.MorningProgram.Split('-')[1].Trim(),
                AfternoonOpening = posGet.AfternoonProgram.Split('-')[0].Trim(),
                AfternoonClosing = posGet.AfternoonProgram.Split('-')[1].Trim()
            };
            return PartialView("EditPos", pos);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePos(PosEditDTO pos)
        {
            //pos.DaysClosed = new List<string>();

            List<string> allDays = Request.Form["dayOfWeek"].ToString().Split(',').ToList();
            pos.DaysClosed = allDays.Where(d => d != "false").ToList();

            await _posRepository.UpdatePos(pos);

            var cities = _posRepository.GetCityList();
            var connectionTypes = _posRepository.GetConnectionTypeList();
            ViewBag.Cities = new SelectList(cities, "Id", "CityName");
            ViewBag.ConnectionTypes = new SelectList(connectionTypes, "Id", "ConnectionType");

            //return PartialView("EditPos");
            return RedirectToAction("GetAllPos");
        }

        [HttpPost]
        public async Task<ActionResult> DeletePos(int id)
        {
            _posRepository.DeletePos(id);
            return RedirectToAction("GetAllPos");
        }
    }
}