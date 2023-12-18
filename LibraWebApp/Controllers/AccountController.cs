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
            return View();
        }

		[HttpPost]
		public  async Task<ActionResult> Login(LoginUserDTO user)
		{
            UserDTO userFromDb = await _userRepository.GetEntityByNameAsync(user.UserName);

			try
			{
				if (!ModelState.IsValid)
				{
					return View(user);
				}
			}
			catch (Exception)
			{

				throw;
			}

			return RedirectToAction("");
		}

		[Authorize]
		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Account");
		}
	}
}