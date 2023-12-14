using LibraBll.DTOs;
using LibraBll.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LibraWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository _userRepository;
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

		[HttpPost]
		public  async Task<ActionResult> Login(LoginUserDTO user)
		{
            UserDTO userFromDb = await _userRepository.GetUserByNameAsync(user.UserName);

            if (userFromDb == null)
            {
                return null;
            }

			return RedirectToAction("");
		}
	}
}