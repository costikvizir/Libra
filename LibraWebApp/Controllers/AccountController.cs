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
using System.Data.Entity.Core.Metadata.Edm;
using Libra.Dal.Context;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.Owin.Security.Cookies;
using System.Security.Claims;
using System.Security.Policy;
using System.Web.UI.WebControls;
using System.Net;
using Microsoft.Owin.Security;

namespace LibraWebApp.Controllers
{
	[Authorize]
    public class AccountController : Controller
    {
		private readonly IRepository<UserDTO> _userRepository;
		LibraContext context = new LibraContext();

		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		public AccountController(IRepository<UserDTO> userRepository)
        {
			_userRepository = userRepository;	
        }

        [HttpGet]
		[AllowAnonymous]
		public ActionResult Login()
		{
			return PartialView();
		}



		[HttpPost]
		[AllowAnonymous]
		public ActionResult Login(UserDTO model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(model);
				}


				using (var userRepository = new UserRepository(context))
				{
					if (userRepository.GetEntityAuth(model.Name, model.Password) is null)
					{
						ModelState.AddModelError("IncorrectLogin", "Credenziali dell'account errate");
						return View(model);
					}
					else
					{
						//var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
						var identity = new ClaimsIdentity(new[]
						{
							new Claim(ClaimTypes.Name, model.Name),
							new Claim(ClaimTypes.Email, model.Email),
							new Claim(ClaimTypes.Role, model.Role)
						}, FormsAuthentication.FormsCookieName);

						AuthenticationManager.SignIn(identity);
						//var identity = (ClaimsIdentity)User.Identity;
						//var claim = identity.FindFirst(ClaimTypes.Name);
						//var principal = new ClaimsPrincipal(identity);

						FormsAuthentication.SetAuthCookie(model.Name, false);
						return RedirectToAction("Index", "User");
					}
				}

				//[HttpPost]
				//[AllowAnonymous]
				//[ValidateAntiForgeryToken]
				//public async Task<IActionResult> Login(string userName, string password)
				//{
				//	try
				//	{
				//		var userVm = await Mediator.Send(new GetUserByUserNameQuery
				//		{
				//			UserName = userName,
				//			Password = password
				//		});

				//		if (userVm == null || !userVm.IsEnabled)
				//		{
				//			ModelState.TryAddModelError("IncorrectLogin", "Non existen or delited user");
				//			return View("Login");
				//		}
				//		else
				//		{
				//			var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

				//			identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userVm.Id.ToString()));
				//			identity.AddClaim(new Claim(ClaimTypes.Name, userVm.UserName));
				//			identity.AddClaim(new Claim("FullName", userVm.FullName));
				//			identity.AddClaim(new Claim(ClaimTypes.Email, userVm.Email));

				//			foreach (var role in userVm.UserRoles)
				//			{
				//				identity.AddClaim(new Claim(ClaimTypes.Role, role));
				//			}

				//			var principal = new ClaimsPrincipal(identity);

				//			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				//			return LocalRedirect("/Home");
				//		}
				//	}
				//	catch (Exception ex)
				//	{
				//		return View("Login");
				//	}
				//}

				//var user = _userRepository.GetEntityAuth(model.Name, model.Password);
				//var user = _userRepository.GetEntityAuth(model.Name, model.Password);

				//if (user is null)
				//{
				//	ModelState.AddModelError("Incorrect Login", "Wrong login or password");
				//	return View(model);
				//}
				//else
				//{
				//	FormsAuthentication.SetAuthCookie(model.Name, false);
				//	return RedirectToAction("Index", "User");
				//}
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


		[Authorize]
		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Account");
		}
	}
}