﻿using FluentValidation;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common.DataTableModels;
using LibraBll.DTOs.ComplexObjects;
using LibraBll.DTOs.Pos;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections;
using System;

namespace LibraWebApp.Controllers
{
    [Authorize(Roles = "Administrator, Technical Support, User")]
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

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetPosById(int id)
        {
            PosGetDTO pos = await _posRepository.GetPosByIdAsync(id);

            List<string> noClosingDays = new List<string> { "No Closing Days" };
            List<string> posWeekDays = (await _posRepository.GetPosClosingDays(id)).Select(d => d.Day).ToList();
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
            //List<PosGetDTO> allPos = await _posRepository.GetAllPosAsync();

            //if (!allPos.Any())
            //    return null;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetAllPosJson(DataTablesParameters parameters = null)
        {
            parameters = parameters ?? new DataTablesParameters();

            List<PosGetDTO> allPos = await _posRepository.GetAllPosAsync(parameters, CancellationToken.None);

            if (!allPos.Any())
                return Json(new { }, JsonRequestBehavior.AllowGet);

            return Json(new
            {
                draw = parameters.Draw,
                recordsFiltered = parameters.TotalCount,
                recordsTotal = parameters.TotalCount,
                data = allPos
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> GetAllPosCustomSearchJson(DataTablesParameters parameters, string Name = "", string Brand = "", string FullAddress = "")
        {
            parameters = parameters ?? new DataTablesParameters();

            // Debug log to verify received parameters
            Console.WriteLine("Received filters - Name: " + Name + ", Brand: " + Brand + ", FullAddress: " + FullAddress);

            // Adjust your repository query to include custom parameters for filtering
            List<PosGetDTO> allPos = await _posRepository.GetAllPosAsync(parameters, Name, Brand, FullAddress, CancellationToken.None);

            if (!allPos.Any())
                return Json(new { }, JsonRequestBehavior.AllowGet);

            return Json(new
            {
                draw = parameters.Draw,
                recordsFiltered = parameters.TotalCount,
                recordsTotal = parameters.TotalCount,
                data = allPos
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> AddPos()
        {
            var cities = await _posRepository.GetCityList();
            var connectionTypes = await _posRepository.GetConnectionTypeList();
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

            var cities = await _posRepository.GetCityList();
            var connectionTypes = await _posRepository.GetConnectionTypeList();
            ViewBag.Cities = new SelectList(cities, "Id", "CityName");
            ViewBag.ConnectionTypes = new SelectList(connectionTypes, "Id", "ConnectionType");

            return RedirectToAction("GetAllPos");
            //return Json(new { success = true, message = "Successfully saved" });
        }

        [HttpGet]
        public async Task<ActionResult> UpdatePos(int id)
        {
            if (id == 0)
                return null;

            var cities = await _posRepository.GetCityList();
            var connectionTypes = await _posRepository.GetConnectionTypeList();
            var posWeekDays = await _posRepository.GetPosClosingDays(id);

            ViewBag.Cities = new SelectList(cities, "Id", "CityName");
            ViewBag.ConnectionTypes = new SelectList(connectionTypes, "Id", "ConnectionType");

            PosGetDTO posGet = await _posRepository.GetPosByIdAsync(id);
            int cityId = (await _posRepository.GetCityList())
                            .Where(c => c.CityName == posGet.City)
                            .Select(c => c.Id)
                            .FirstOrDefault();
            int connectionTypeId = (await _posRepository.GetConnectionTypeList())
                            .FirstOrDefault(c => c.ConnectionType == posGet.ConnectionType).Id;

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

            return View("EditPos", pos);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePos(PosEditDTO pos)
        {
            //pos.DaysClosed = new List<string>();

            List<string> allDays = Request.Form["dayOfWeek"].ToString().Split(',').ToList();
            pos.DaysClosed = allDays.Where(d => d != "false").ToList();

            await _posRepository.UpdatePos(pos);

            var cities = await _posRepository.GetCityList();
            var connectionTypes = await _posRepository.GetConnectionTypeList();
            ViewBag.Cities = new SelectList(cities, "Id", "CityName");
            ViewBag.ConnectionTypes = new SelectList(connectionTypes, "Id", "ConnectionType");

            //return PartialView("EditPos");
            //return RedirectToAction("GetAllPos");
            return Json(new { success = true, message = "Successfully edited" });
        }

        //[HttpPost]
        //public async Task<ActionResult> DeletePos(int id)
        //{
        //    _posRepository.DeletePos(id);
        //    return RedirectToAction("GetAllPos");
        //}

        [HttpPost]
        public ActionResult DeletePos(int id)
        {
            _posRepository.DeletePos(id);
            return RedirectToAction("GetAllPos");
        }
    }
}