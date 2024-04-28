using Libra.Dal.Entities;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common;
using LibraBll.Common.DataTableModels;
using LibraBll.Common.Extensions;
using LibraBll.DTOs.Dropdown;
using LibraBll.DTOs.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
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

        public async Task<List<GetUserDTO>> GetAllUsers(DataTablesParameters parameters, CancellationToken cancellationToken)
        {
            //parameters = parameters ?? new UserDataTableParameters();
            //parameters.TotalCount = await Context.Users.CountAsync(x => x.IsDeleted == false);
            ////parameters.SetColumnName();
            //parameters.Draw = 4;
            ////parameters.Start = parameters.Start < 0 ? 0 : parameters.Start;
            //// parameters.Length = parameters.Length < 0 ? 0 : parameters.Length;
            //parameters.Start = 0;
            //parameters.Length = 7;
            //parameters.Order = new List<DataTablesOrder>
            //{
            //    new DataTablesOrder
            //    {
            //        Column = 0,
            //        Dir = "asc"
            //    }
            //};
            //parameters.Search = new DataTablesSearch
            //{
            //    Value = "",
            //    Regex = ""
            //};
            //parameters.Columns = new List<DataTablesColumn>
            //{
            //    new DataTablesColumn
            //    {
            //        Data = "Name",
            //        Name = "Name",
            //        Orderable = true,
            //        Searchable = true
            //    },
            //    new DataTablesColumn
            //    {
            //        Data = "Login",
            //        Name = "Login",
            //        Orderable = true,
            //        Searchable = true
            //    },
            //    new DataTablesColumn
            //    {
            //        Data = "Email",
            //        Name = "Email",
            //        Orderable = true,
            //        Searchable = true
            //    },
            //    new DataTablesColumn
            //    {
            //        Data = "Telephone",
            //        Name = "Telephone",
            //        Orderable = true,
            //        Searchable = true
            //    },
            //    new DataTablesColumn
            //    {
            //        Data = "Role",
            //        Name = "Role",
            //        Orderable = true,
            //        Searchable = true
            //    }
            //};
            // parameters.Order =
            // parameters.Start

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
                .AsQueryable()
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
            await Context.SaveChangesAsync();

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

        public List<RoleDTO> GetRoles()
        {
            List<RoleDTO> roles = Context.UserTypes
                .Select(x => new RoleDTO
                {
                    Id = x.Id,
                    Role = x.Role
                })
                .ToList();

            return roles;
        }
    }
}