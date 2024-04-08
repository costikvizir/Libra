using Libra.Dal.Context;
using Libra.Dal.Entities;
using LibraBll.Abstractions;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common;
using LibraBll.DTOs;
using LibraBll.DTOs.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibraBll.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository() : base()
        {
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            User entity = await Context.Users.FindAsync(id);

            var user = new UserDTO()
            {
                Name = entity.Name,
                Email = entity.Email,
                Telephone = entity.Telephone,
                UserTypeId = entity.UserTypeId
            };

            return user;
        }

        public async Task<UserDTO> GetUserByNameAsync(string name)
        {
            User entity = await Context.Users.FindAsync(name);
            if (entity != null)
            {
                var user = new UserDTO()
                {
                    Name = entity.Name,
                    Email = entity.Email,
                    Password = entity.Password,
                    Telephone = entity.Telephone,
                    UserTypeId = entity.UserTypeId
                };
                return user;
            }

            return null;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            List<UserDTO> userList = null;
            try
            {
                userList = await Context.Users
                .Where(x => x.IsDeleted == false)
                .Include(x => x.UserType)
                .Select(x => new UserDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Login = x.Login,
                    Email = x.Email,
                    Telephone = x.Telephone,
                    UserTypeId = x.UserTypeId,
                    Role = x.UserType.Role,
                })
                .ToListAsync();
            }
            catch (Exception e)
            {
                throw;
            }

            return userList;
        }

        public async Task<AddUserDTO> CreateUser(AddUserDTO userPost)
        {
            //map role to userTypeId and set default user to "User"
            var userTypeID = Context.UserTypes.FirstOrDefault(x => x.Role == userPost.Role)?.Id ?? 3;

            User user = new User()
            {
                Name = userPost.Name,
                Email = userPost.Email,
                Telephone = userPost.Telephone,
                UserTypeId = userTypeID,
                Login = userPost.Login,
                Password = userPost.Password,
                //IsDeleted = userPost.IsActive
            };

            Context.Users.Add(user);
            await Context.SaveChangesAsync();

            return userPost;
        }

        public void UpdateUser(UserDTO userPost)
        {
            var userTypeId = Context.UserTypes.FirstOrDefault(x => x.Role == userPost.Role)?.Id ?? 3;
            User user = new User()
            {
                Name = userPost.Name,
                Email = userPost.Email,
                Telephone = userPost.Telephone,
                UserTypeId = userTypeId,
                Login = userPost.Login,
                Password = userPost.Password
            };

            Context.Users.Update(user);
            Context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            User user = Context.Users.FirstOrDefault(x => x.Id == id);

            if (user != null)
                user.IsDeleted = true;

            Context.Users.Update(user);
            Context.SaveChanges();
        }

        public async Task<UserDTO> GetUserAuth(string name, string password)
        {
            User entity = null;
            try
            {
                entity = await Context.Users
                           .Include(x => x.UserType)
                           .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Name.ToUpper() == name.ToUpper() && x.Password == password);
            }
            catch (Exception ex)
            {
            }

            if (entity != null)
            {
                var user = new UserDTO()
                {
                    Name = entity.Name,
                    Email = entity.Email,
                    Password = entity.Password,
                    Telephone = entity.Telephone,
                    UserTypeId = entity.UserTypeId,
                    Login = entity.Login,
                    Role = entity.UserType.Role
                };
                return user;
            }

            return null;
        }
    }
}