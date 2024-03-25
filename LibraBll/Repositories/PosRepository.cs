using LibraBll.Abstractions;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common;
using LibraBll.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.Repositories
{
	public class PosRepository : BaseRepository, IPosRepository
	{
		public Task<PosDTO> CreatePos(PosDTO pos)
		{
			throw new NotImplementedException();
		}

		public void DeletePos(string name)
		{
			throw new NotImplementedException();
		}

		public Task<List<PosDTO>> GetAllPosAsync()
		{
			throw new NotImplementedException();
		}

		public Task<PosDTO> GetPosByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public void UpdatePos(PosDTO pos)
		{
			throw new NotImplementedException();
		}
	}
}
