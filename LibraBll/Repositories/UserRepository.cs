using Libra.Dal.Context;
using Libra.Dal.Entities;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common;
using LibraBll.Common.DataTableModels;
using LibraBll.Common.Extensions;
using LibraBll.DTOs.Dropdown;
using LibraBll.DTOs.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LibraBll.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(LibraContext context) : base(context)
        {
        }

        public async Task<GetUserDTO> GetUserByIdAsync(int id)
        {
            User entity = await Context.Users.FindAsync(id);
            //var userRole =
            var user = new GetUserDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                Login = entity.Login,
                Password = entity.Password,
                Email = entity.Email,
                Telephone = entity.Telephone,
                UserTypeId = entity.UserTypeId,
                Role = entity.UserType.Role
            };

            return user;
        }

        public async Task<GetUserDTO> GetUserByNameAsync(string name)
        {
            User entity = await Context.Users.FindAsync(name);
            if (entity != null)
            {
                var user = new GetUserDTO()
                {
                    Name = entity.Name,
                    Email = entity.Email,
                    Password = entity.Password,
                    Telephone = entity.Telephone,
                    UserTypeId = entity.UserTypeId,
                    Role = Context.UserTypes.FirstOrDefault(x => x.Id == entity.UserTypeId).Role
                };
                return user;
            }

            return null;
        }

        public async Task<IEnumerable<GetUserDTO>> GetAllUsers(DataTablesParameters parameters)
        {
            List<GetUserDTO> userList = null;
            try
            {
                userList = await Context.Users
                .Where(x => x.IsDeleted == false)
                .Include(x => x.UserType)
                .Select(x => new GetUserDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Login = x.Login,
                    Email = x.Email,
                    Telephone = x.Telephone,
                    UserTypeId = x.UserTypeId,
                    Role = x.UserType.Role,
                })
                //.AsQueryable()
                .Search(parameters)
                .OrderBy(parameters)
                .Page(parameters)
                .ToListAsync();
            }
            catch (Exception e)
            {
                throw;
            }

            return userList;
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await Context.Users.CountAsync(x => x.IsDeleted == false);
        }

        public async Task<AddUserDTO> CreateUser(AddUserDTO userPost)
        {
            User user = new User()
            {
                Name = userPost.Name,
                Email = userPost.Email,
                Telephone = userPost.Telephone,
                UserTypeId = userPost.Role,
                Login = userPost.Login,
                Password = userPost.Password,
                //IsDeleted = userPost.IsActive
            };

            Context.Users.Add(user);
            //_logger.Information($"{user.Name} added to context");
            await Context.SaveChangesAsync();
            //_logger.Information($"{user.Name} saved in database");

            return userPost;
        }

        public void UpdateUser(ModifyUserDTO userPost)
        {
            var user = Context.Users.FirstOrDefault(x => x.Id == userPost.Id);

            if (user == null)
                return;
            user.Name = userPost.Name;
            user.Email = userPost.Email;
            user.Telephone = userPost.Telephone;
            user.UserTypeId = userPost.Role;
            user.Login = userPost.Login;
            // user.Password = userPost.Password;

            //Context.Users.Update(user);
            //Context.Entry(user).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            User user = Context.Users.FirstOrDefault(x => x.Id == id);

            if (user != null)
                user.IsDeleted = true;

            //Context.Entry(user).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public async Task<LoginUserDTO> GetUserAuth(string name, string password)
        {
            User entity = null;
            try
            {
                entity = await Context.Users
                           .Include(x => x.UserType)
                           .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Name.ToUpper() == name.ToUpper() && x.Password == password);

                //entity.UserType = Context.UserTypes.FirstOrDefault(x => x.Id == entity.UserTypeId).Role.ToString();
                //TODO
                //var userRole = Context.UserTypes.FirstOrDefault(x => x.Id == entity.UserTypeId);
                //var role = userRole.Role.ToString();
            }
            catch (Exception ex)
            {
            }

            if (entity != null)
            {
                var userRole = await Context.UserTypes.FirstOrDefaultAsync(x => x.Id == entity.UserTypeId);
                var user = new LoginUserDTO()
                {
                    UserName = entity.Name,
                    Email = entity.Email,
                    Password = entity.Password,
                    Login = entity.Login,
                    Role = userRole.Role.ToString()
                };
                return user;
            }

            return null;
        }

        //TODO async
        public async Task<IEnumerable<RoleDTO>> GetRoles()
        {
            IEnumerable<RoleDTO> roles = await Context.UserTypes
                .Select(x => new RoleDTO
                {
                    Id = x.Id,
                    Role = x.Role
                })
                .ToListAsync();

            return roles;
        }
    }
}