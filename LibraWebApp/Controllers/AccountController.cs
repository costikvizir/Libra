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

namespace LibraWebApp.Controllers
{
    public class AccountController : Controller
    {
		private readonly IRepository<UserDTO> _userRepository;
		[HttpGet]
		public ActionResult Login()
		{
			return PartialView();
		}

		[HttpPost]
		public ActionResult Login(LoginUserDTO model)
		{
			if (!ModelState.IsValid)
			{
				// Perform the login operation...

				return RedirectToAction("Index", "Home");
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		//[HttpPost]
		//public async Task<ActionResult> Login(LoginUserDTO user)
		//{
		//	UserDTO userFromDb = await _userRepository.GetEntityByNameAsync(user.UserName);
		//	try
		//	{
		//		if (!ModelState.IsValid)
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

		//[Authorize]
		//public ActionResult LogOut()
		//{
		//	FormsAuthentication.SignOut();
		//	return RedirectToAction("Login", "Account");
		//}
	}
}