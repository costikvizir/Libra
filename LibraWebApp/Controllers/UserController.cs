using FluentValidation;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common.DataTableModels;
using LibraBll.DTOs.User;
using LibraBll.Validators.User;
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
        private readonly IValidator<ModifyUserDTO> _modifyUserValidator;

        public UserController(IUserRepository userRepository, IValidator<AddUserDTO>
            createUserValidator, IValidator<ModifyUserDTO> modifyUserValidator)
        {
            _userRepository = userRepository;
            _createUserValidator = createUserValidator;
            _modifyUserValidator = modifyUserValidator;
        }

        //TODO: Put all modal windows in separate files

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
            var roles = await _userRepository.GetRoles();
            //var roles = await _userRepository.GetRolesCachedAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Role");
            return View();
        }

        // [Authorize]
        [HttpPost]
        public async Task<JsonResult> GetAllUsersJson(DataTablesParameters parameters = null)
        {
            parameters = parameters ?? new DataTablesParameters();

            IEnumerable<GetUserDTO> users = await _userRepository.GetAllUsers(parameters);

            return Json(new
            {
                draw = parameters.Draw,
                recordsFiltered = parameters.TotalCount,
                recordsTotal = parameters.TotalCount,
                data = users
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> AddUser()
        {
            var roles = await _userRepository.GetRoles();
            //var roles = await _userRepository.GetRolesCachedAsync();
            //ViewBag.Roles = new SelectList(roles, "Id", "Role");

            ViewBag.Roles = roles.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Role
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(AddUserDTO user)
        {
            try
            {
                var results = await _createUserValidator.ValidateAsync(user);
                if (!results.IsValid)
                {
                    foreach (var failure in results.Errors)
                    {
                        ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                    }
                    var roles = await _userRepository.GetRoles();
                    //ViewBag.Roles = new SelectList(roles, "Id", "Role");
                    ViewBag.Roles = roles.Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Role
                    }).ToList();
                    return PartialView("AddUser", user);
                }
            }
            catch (System.Exception ex)
            {

                throw;
            }

            //try
            //{
            //    var validator = new AddUserDTOValidator(_userRepository);
            //    await validator.InitializeAsync();
            //    validator.SetupRules();

            //    var results = await validator.ValidateAsync(user);
            //    if (!results.IsValid)
            //    {
            //        foreach (var failure in results.Errors)
            //        {
            //            ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
            //        }

            //        ViewBag.Roles = validator.CachedRoles.Select(r => new SelectListItem
            //        {
            //            Value = r.Id.ToString(),
            //            Text = r.Role
            //        }).ToList();

            //        return PartialView("AddUser", user);
            //    }

            //    // Proceed with the rest of your logic if validation passes
            //    // ...

            //    return View("UserList"); // Example: Redirect to a user list view
            //}
            //catch (System.Exception ex)
            //{
            //    // Log the exception
            //    // Logger.LogError(ex, "An error occurred while creating a user.");
            //    throw;
            //}

            await _userRepository.CreateUser(user);

           

            return RedirectToAction("GetAllUsers");
        }

        [HttpGet]
        public async Task<ActionResult> UpdateUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            var roles = await _userRepository.GetRoles();
            //var roles = await _userRepository.GetRolesCachedAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Role");

            return PartialView("~/Views/User/_Edit.cshtml", user);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUser(ModifyUserDTO user)
        {
            var modifiedUser = new ModifyUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Login = user.Login,
                Role = user.Role,
                Password = user.Password,
                IsActive = user.IsActive,
                Telephone = user.Telephone,
            };

            var results = _modifyUserValidator.Validate(modifiedUser);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return PartialView("GetAllUsers");
            }

            _userRepository.UpdateUser(user);

            var roles = await _userRepository.GetRoles();
            //var roles = await _userRepository.GetRolesCachedAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Role");

            return PartialView("GetAllUsers");
            //return Json(new { success = true, message = "Successfully saved" });
        }

        [HttpPost]
        [Route("User/DeleteUser/{id}")]
        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }

        [HttpPost]
        public async Task<JsonResult> IsUserNameAvailable(string userName)
        {
            bool isAvailable = await _userRepository.UserNameExistsAsync(userName);
            return Json(isAvailable);
        }
    }
}