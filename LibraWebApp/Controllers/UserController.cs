using LibraBll.Abstractions;
using LibraBll.Abstractions.Repositories;
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
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult Index(UserDTO model)
        {
            var user = User.Identity.Name;
            return View(model.Name);
        }

		[HttpGet]
        public async Task<ActionResult> GetUserByName(string name)
        {
            var userFromDb = await _userRepository.GetUserByNameAsync(name);
            return View(userFromDb);    
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            List<UserDTO> allUsers = await _userRepository.GetAllUsersAsync();

            if (!allUsers.Any())
                return null;

            return PartialView(allUsers);
        }

        [Authorize]
        [HttpGet]
        public async Task<JsonResult> GetAllUsersJson()
        {
            List<UserDTO> allUsers = await _userRepository.GetAllUsersAsync();

			if (!allUsers.Any())
				return Json(new { }, JsonRequestBehavior.AllowGet);

			return Json(allUsers, JsonRequestBehavior.AllowGet);
        }

		[HttpGet]
		public ActionResult AddUser()
		{
			return View();
		}

		[HttpPost]
        public async Task<ActionResult> AddUser(UserDTO user)
        {
            await _userRepository.CreateUser(user);
            return PartialView();
        }

        [HttpPost]
        public Task<ActionResult> DeleteUser(string userName)
        {
            _userRepository.DeleteUser(userName);
			return null;
		}
    }
}