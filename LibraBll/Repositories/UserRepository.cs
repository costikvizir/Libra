using Libra.Dal.Entities;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common;
using LibraBll.DTOs.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraBll.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository() : base()
        {
        }

        public async Task<GetUserDTO> GetUserByIdAsync(int id)
        {
            User entity = await Context.Users.FindAsync(id);

            var user = new GetUserDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                Login = entity.Login,
                Password = entity.Password,
                Email = entity.Email,
                Telephone = entity.Telephone,
                UserTypeId = entity.UserTypeId
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
                    UserTypeId = entity.UserTypeId
                };
                return user;
            }

            return null;
        }

        public async Task<List<GetUserDTO>> GetAllUsersAsync()
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

        public void UpdateUser(ModifyUserDTO userPost)
        {
            var userTypeId = Context.UserTypes.FirstOrDefault(x => x.Role == userPost.Role)?.Id ?? 3;
            var user = Context.Users.FirstOrDefault(x => x.Id == userPost.Id);

            if (user == null)
                return;
            user.Name = userPost.Name;
            user.Email = userPost.Email;
            user.Telephone = userPost.Telephone;
            user.UserTypeId = userTypeId;
            user.Login = userPost.Login;
            // user.Password = userPost.Password;

            //Context.Users.Update(user);
            Context.Entry(user).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            User user = Context.Users.FirstOrDefault(x => x.Id == id);

            if (user != null)
                user.IsDeleted = true;

            Context.Entry(user).State = EntityState.Modified;
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
            }
            catch (Exception ex)
            {
            }

            if (entity != null)
            {
                var user = new LoginUserDTO()
                {
                    UserName = entity.Name,
                    Email = entity.Email,
                    Password = entity.Password,
                    Login = entity.Login,
                    Role = entity.UserType.Role
                };
                return user;
            }

            return null;
        }
    }
}