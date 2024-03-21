﻿using LibraBll.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.Abstractions.Repositories
{
	public interface IUserRepository
	{
		Task<UserDTO> GetUserByIdAsync(int id);
		Task<UserDTO> GetUserByNameAsync(string name);
		Task<List<UserDTO>> GetAllUsersAsync();
		Task<UserDTO> CreateUser(UserDTO userPost);
		Task<UserDTO> GetUserAuth(string name, string password);
		void UpdateUser(UserDTO userPost);
		void DeleteUser(string name);
	}
}
