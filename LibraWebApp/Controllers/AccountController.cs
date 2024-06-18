using LibraBll.Abstractions.Repositories;
using LibraBll.DTOs.User;
using Microsoft.Owin.Security;
using System;

//using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
        public async Task<ActionResult> Login(LoginUserDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // ViewBag.ShowErrorMessage = true;
                    return View(model);
                }

                using ((System.IDisposable)_userRepository)
                {
                    var user = await _userRepository.GetUserAuth(model.UserName, model.Password);
                    if (user is null)
                    {
                        //ModelState.AddModelError("IncorrectLogin", "Credenziali dell'account errate");
                        ViewBag.ShowErrorMessage = true;
                        return View(model);
                    }
                    else
                    {
                        //var userRole = await _userRepository.GetUserRole(user.Id);

                        ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                        claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Role.ToString(), ClaimValueTypes.String));
                        claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName, ClaimValueTypes.String));
                        claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                            "OWIN Provider", ClaimValueTypes.String));

                        if (user.Role != null)
                            claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role, ClaimValueTypes.String));

                        AuthenticationManager.SignOut();

                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, claim);

                        ViewBag.UseNullLayout = true;
                        return RedirectToAction("Index", "Home");
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