using LibraBll.Abstractions.Repositories;
using LibraBll.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LibraWebApp.Controllers
{
    public class PosController : Controller
    {
        private readonly IPosRepository _posRepository;

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
        public async Task<ActionResult> AllPos()
        {
            List<PosDTO> allPos = await _posRepository.GetAllPosAsync();

            if (!allPos.Any())
                return null;

            return View(allPos);
        }

        [HttpGet]
        public async Task<JsonResult> AllPosJson()
        {
            List<PosDTO> allPos = await _posRepository.GetAllPosAsync();

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
        public async Task<ActionResult> AddPos(PosDTO pos)
        {
            await _posRepository.AddPosAsync(pos);
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePos(PosDTO pos)
        {
            _posRepository.UpdatePos(pos);
            return PartialView();
        }

        [HttpPost]
        public Task<ActionResult> DeletePos(string name)
        {
            _posRepository.DeletePos(name);
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