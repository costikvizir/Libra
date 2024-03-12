using LibraBll.Abstractions;
using LibraBll.DTOs;
using LibraBll.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;

namespace LibraWebApp.Controllers
{
	[Authorize]
    public class AccountController : Controller
    {
		private readonly IRepository<UserDTO> _userRepository;

		[HttpGet]
		[AllowAnonymous]
		public ActionResult Login()
		{
			return PartialView();
		}

		[HttpPost]
		[AllowAnonymous]
		public ActionResult Login(LoginUserDTO model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(model);
				}

				//using (userRepository = new UserRepository())
				//{
				//	if (userRepository.GetUser(model.UserName, model.Password) is null)
				//	{
				//		ModelState.AddModelError("IncorrectLogin", "Credenziali dell'account errate");
				//		return View(model);
				//	}
				//	else
				//	{
				//		FormsAuthentication.SetAuthCookie(model.UserName, false);
				//		return RedirectToAction("Index", "User");
				//	}
				//}
				if (_userRepository.GetEntityByNameAsync(model.UserName) is null)
				{
					ModelState.AddModelError("Incorrect Login", "Wrong login or password");
					return View(model);
				}
				else
				{
					FormsAuthentication.SetAuthCookie(model.UserName, false);
					return RedirectToAction("Index", "User");
				}
			}
			catch (Exception ex)
			{
				//logger.Error(ex, "Account/Lgoin");
				//return CreateJsonError();
				var errorData = new { Success = false, Message = "An error occurred while processing your request." };
				return Json(errorData, JsonRequestBehavior.AllowGet);
			}
			//if (!ModelState.IsValid)
			//{
			//	// Perform the login operation...

			//	return RedirectToAction("Index", "Home");
			//}

			// If we got this far, something failed, redisplay form
			//return View(model);
		}


		//[HttpPost]
		//[AllowAnonymous]
		//public async Task<ActionResult> Login(LoginUserDTO user)
		//{
		//	UserDTO userFromDb = await _userRepository.GetEntityByNameAsync(user.UserName);
		//	try
		//	{
		//		if (ModelState.IsValid)
		//		{
		//			return View(user);
		//		}
		//	}
		//	catch (Exception)
		//	{

		//		throw;
		//	}

		//	return RedirectToAction("Index", "Home");
		//}

		[Authorize]
		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Account");
		}
	}
}