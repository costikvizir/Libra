using FluentValidation;
using LibraBll.Abstractions.Repositories;
using LibraBll.DTOs.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LibraWebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<AddUserDTO> _createUserValidator;

        public UserController(IUserRepository userRepository, IValidator<AddUserDTO> createUserValidator)
        {
            _userRepository = userRepository;
            _createUserValidator = createUserValidator;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var user = User.Identity.Name;
            return View();
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
            return View("AddUser");
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(AddUserDTO user)
        {
            var results = _createUserValidator.Validate(user);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return PartialView();
            }
            await _userRepository.CreateUser(user);
            return PartialView();
        }

        [HttpGet]
        public async Task<ActionResult> UpdateUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return View("~/Views/User/_Edit.cshtml", user);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUser(UserDTO user)
        {
            _userRepository.UpdateUser(user);
            return PartialView();
        }

        [HttpPost]
        [Route("User/DeleteUser/{id}")]
        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }
    }
}