﻿using LibraBll.Abstractions;
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
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly IRepository<UserDTO> _userRepository;

        public UserController(IRepository<UserDTO> userRepository)
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
            var userFromDb = await _userRepository.GetEntityByNameAsync(name);

            return View(userFromDb);    
        }

       // [Authorize]
		[HttpGet]
		public async Task<ActionResult> GetAllUsers()
		{
			List<UserDTO> allUsers = await _userRepository.GetAllEntitiesAsync();

            
            
            
            if (!allUsers.Any())
                return null;

            return PartialView(allUsers);
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