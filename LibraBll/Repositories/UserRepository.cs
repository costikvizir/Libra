using Libra.Dal.Entities;
using LibraBll.Common;
using LibraBll.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.Repositories
{
	public class UserRepository : BaseRepository
	{ 
		public async Task<UserDTO> GetUserByIdAsync(int id)
		{
			var entity = await Context.Users.FindAsync(id);

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
			var entity = await Context.Users.FindAsync(name);

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

		public async Task<List<UserDTO>> GetAllUsersAsync()
		{
			var users = await Context.Users.ToListAsync();

			List<UserDTO> result = new List<UserDTO>();	

			foreach (var user in users)
			{
				UserDTO model = new UserDTO()
				{
					Name = user.Name,
					Email = user.Email,
					Telephone = user.Telephone,
					UserTypeId = user.UserTypeId
				};

				result.Add(model);
			}

			return result;
		}

		public async Task<User> CreateUserAsync(UserDTO userPost)
		{
			User user = new User()
			{
				Name = userPost.Name,
				Email = userPost.Email,
				Telephone = userPost.Telephone,
				UserTypeId = userPost.UserTypeId,
				Login = userPost.Login,
				Password = userPost.Password
			};

		    Context.Users.Add(user);	
			await Context.SaveChangesAsync();

			return user;
		}

		public async Task<User> UpdateUserAsync(UserDTO userPost)
		{
			User user = new User()
			{
				Name = userPost.Name,
				Email = userPost.Email,
				Telephone = userPost.Telephone,
				UserTypeId = userPost.UserTypeId,
				Login = userPost.Login,
				Password = userPost.Password
			};

			Context.Users.Update(user);
			await Context.SaveChangesAsync();

			return user;
		}

		public async Task<User> RemoveUserByIdAsync(int id)
		{
			var userToDelete = await Context.Users.FindAsync(id);

			if (userToDelete != null)
				return null;

			Context.Remove(userToDelete);
			await Context.SaveChangesAsync();	

			return userToDelete;
		}
	}
}
