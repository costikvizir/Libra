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
using LibraBll.Abstractions.Repositories;

namespace LibraWebApp.Controllers
{
	[Authorize]
    public class AccountController : Controller
    {
		private readonly IUserRepository _userRepository;
		//private LibraContext context = new LibraContext();

		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		public AccountController(IUserRepository userRepository)
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
		public async Task<ActionResult> Login(UserDTO model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
                   // ViewBag.ShowErrorMessage = true;
                    return View(model);
				}				

				using ((System.IDisposable) _userRepository)
				{
					var user = await _userRepository.GetUserAuth(model.Name, model.Password);
					if (user is null)
					{
						//ModelState.AddModelError("IncorrectLogin", "Credenziali dell'account errate");
                        ViewBag.ShowErrorMessage = true;
                        return View(model);
					}
					else
					{

						ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
						claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Role.ToString(), ClaimValueTypes.String));
						claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
						claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
							"OWIN Provider", ClaimValueTypes.String));

						if (user.Role != null)
							claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role, ClaimValueTypes.String));

						AuthenticationManager.SignOut();

						AuthenticationManager.SignIn(new AuthenticationProperties
						{
							IsPersistent = true
						}, claim);

						return RedirectToAction("Index", "User");
					}
				}
			}
			catch (Exception ex)
			{
				//logger.Error(ex, "Account/Lgoin");
				//return CreateJsonError();
				//var errorData = new { Success = false, Message = "An error occurred while processing your request." };
				var errorData = new { Success = false, Message = ex.Message };	
				return Json(errorData, JsonRequestBehavior.AllowGet);
			}
		}


		[Authorize]
		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Account");
		}
	}
}