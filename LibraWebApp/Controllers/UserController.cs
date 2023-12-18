using LibraBll.Abstractions;
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
        private readonly IRepository<UserDTO> _userRepository;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetUserByName(string name)
        {
            var userFromDb = await _userRepository.GetEntityByNameAsync(name);

            return View(userFromDb);    
        }

		[HttpGet]
		public async Task<ActionResult> GetAllUsers()
		{
			var allUsers = await _userRepository.GetAllEntitiesAsync();

            if(!allUsers.Any())
                return null;

			return View(allUsers);
		}

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserDTO user)
        {
            await _userRepository.CreateEntity(user);

            return null;
        }

        [HttpPost]
        public Task<ActionResult> DeleteUser(string userName)
        {
            _userRepository.DeleteEntity(userName);
			return null;
		}
    }
}