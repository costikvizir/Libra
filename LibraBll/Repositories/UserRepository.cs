using Libra.Dal.Context;
using Libra.Dal.Entities;
using LibraBll.Abstractions;
using LibraBll.Common;
using LibraBll.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibraBll.Repositories
{
	public class UserRepository : BaseRepository, IRepository<UserDTO>
	{
		public UserRepository(LibraContext context) : base(context)
		{
		}

		public async Task<UserDTO> GetEntityByIdAsync(int id)
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

		public async Task<UserDTO> GetEntityByNameAsync(string name)
		{
			var entity = await Context.Users.FindAsync(name);
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

		public async Task<List<UserDTO>> GetAllEntitiesAsync()
		{

			List<UserDTO> userList = await Context.Users
				.Select(x => new UserDTO
				{
					Name = x.Name,
					Email = x.Email,
					Telephone = x.Telephone,
					UserTypeId = x.UserTypeId
				})
				.ToListAsync();

			var userTest = userList;

			return userList;
		}

		public async Task<UserDTO> CreateEntity(UserDTO userPost)
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

			return userPost;
		}

		public void UpdateEntity(UserDTO userPost)
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
			Context.SaveChanges();
		}

		public void DeleteEntity(string name)
		{
			User user = Context.Users.FirstOrDefault(x => x.Name == name);

			if (user != null) 
				user.IsDeleted = true;

			Context.Users.Update(user);
			Context.SaveChanges();
		}
	}
}

