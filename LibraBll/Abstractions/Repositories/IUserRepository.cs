using LibraBll.Common.DataTableModels;
using LibraBll.DTOs.Dropdown;
using LibraBll.DTOs.User;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraBll.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<GetUserDTO> GetUserByIdAsync(int id);

        Task<GetUserDTO> GetUserByNameAsync(string name);

        List<GetUserDTO> GetAllUsers(DataTablesParameters parameters, CancellationToken cancellationToken);

        Task<AddUserDTO> CreateUser(AddUserDTO userPost);

        Task<LoginUserDTO> GetUserAuth(string name, string password);

        void UpdateUser(ModifyUserDTO userPost);

        void DeleteUser(int id);

        Task<int> GetUsersCountAsync();

        List<RoleDTO> GetRoles();
    }
}