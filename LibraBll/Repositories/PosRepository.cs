using LibraBll.Abstractions;
using LibraBll.Common;
using LibraBll.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.Repositories
{
	public class PosRepository : IRepository<PosDTO>
	{
		void IRepository.CreateEntity(UserDTO userPost)
		{
			throw new NotImplementedException();
		}

		void IRepository.DeleteEntity(string name)
		{
			throw new NotImplementedException();
		}

		Task<List<UserDTO>> IRepository.GetAllEntitiesAsync()
		{
			throw new NotImplementedException();
		}

		Task<UserDTO> IRepository.GetEntityByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		Task<UserDTO> IRepository.GetEntityByNameAsync(string name)
		{
			throw new NotImplementedException();
		}

		void IRepository.UpdateEntity(UserDTO userPost)
		{
			throw new NotImplementedException();
		}
	}
}
