using FluentValidation;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common.DataTableModels;
using LibraBll.DTOs.User;
using System.Threading;
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
            return View();
        }

        // [Authorize]
        [HttpPost]
        public async Task<JsonResult> GetAllUsersJson(DataTablesParameters parameters = null)
        {
            parameters = parameters ?? new DataTablesParameters();

            //parameters.Order[0].Name = "Id";
            //parameters.Order[0].Dir = "asc";
           // parameters.Columns[0].Data = "Id";

            //parameters.Order[1].Name = "name";
            //parameters.Order[1].Dir = "asc";
            //parameters.Columns[1].Data = "Name";

            var users = await _userRepository.GetAllUsers(parameters, CancellationToken.None);
            return Json(new
            {
                draw = parameters.Draw,
                recordsFiltered = parameters.Length,
                recordsTotal = parameters.TotalCount,
                data = users
            }, JsonRequestBehavior.AllowGet);
        }

        //[Authorize]
        //[HttpGet]
        //public async Task<JsonResult> GetAllUsersJson(JQueryDataTableParams param)
        //{
        //    List<GetUserDTO> allUsers = await _userRepository.GetAllUsersAsync();

        //    if (!allUsers.Any())
        //        return Json(new { }, JsonRequestBehavior.AllowGet);

        //    return Json(allUsers, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public ActionResult AddUser()
        {
            var roles = _userRepository.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Role");

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

            var roles = _userRepository.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Role");

            return PartialView();
        }

        [HttpGet]
        public async Task<ActionResult> UpdateUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            var roles = _userRepository.GetRoles();
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

            var roles = _userRepository.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Role");

            return PartialView("GetAllUsers");
        }

        [HttpPost]
        [Route("User/DeleteUser/{id}")]
        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }
    }
}