using Libra.Dal.Entities;
using LibraBll.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.Abstractions
{
	public interface IRepository<T> where T : class
	{
		Task<T> GetEntityByIdAsync(int id);
		Task<T> GetEntityByNameAsync(string name);
		Task<List<T>> GetAllEntitiesAsync();
		Task<T> CreateEntity(T userPost);
		void UpdateEntity(T userPost);
		void DeleteEntity(string name);
	}
}
