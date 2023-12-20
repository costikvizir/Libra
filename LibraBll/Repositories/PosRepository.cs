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
		public Task<PosDTO> CreateEntity(PosDTO userPost)
		{
			throw new NotImplementedException();
		}

		public void DeleteEntity(string name)
		{
			throw new NotImplementedException();
		}

		public Task<List<PosDTO>> GetAllEntitiesAsync()
		{
			throw new NotImplementedException();
		}

		public Task<PosDTO> GetEntityByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<PosDTO> GetEntityByNameAsync(string name)
		{
			throw new NotImplementedException();
		}

		public void UpdateEntity(PosDTO userPost)
		{
			throw new NotImplementedException();
		}
	}
}
