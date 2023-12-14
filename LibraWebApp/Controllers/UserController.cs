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
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetUserByName(string name)
        {
            var userFromDb = await _userRepository.GetUserByNameAsync(name);

            return View(userFromDb);    
        }

		[HttpGet]
		public async Task<ActionResult> GetAllUsers()
		{
			var allUsers = await _userRepository.GetAllUsersAsync();

			return View(allUsers);
		}

        //[HttpPost]
        //public async Task<ActionResult> CreateUser( UserDTO user)
        //{

        //}
	}
}