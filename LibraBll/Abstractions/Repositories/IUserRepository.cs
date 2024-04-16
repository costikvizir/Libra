using LibraBll.DTOs.Dropdown;
using LibraBll.DTOs.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraBll.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<GetUserDTO> GetUserByIdAsync(int id);

        Task<GetUserDTO> GetUserByNameAsync(string name);

        Task<List<GetUserDTO>> GetAllUsersAsync();

        Task<AddUserDTO> CreateUser(AddUserDTO userPost);

        Task<LoginUserDTO> GetUserAuth(string name, string password);

        void UpdateUser(ModifyUserDTO userPost);

        void DeleteUser(int id);

        List<RoleDTO> GetRoles();
    }
}