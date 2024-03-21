using LibraBll.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.Abstractions.Repositories
{
	public interface IPosRepository
	{
		Task<PosDTO> GetPosByIdAsync(int id);
		Task<List<PosDTO>> GetAllPosAsync();
		Task<PosDTO> CreatePos(PosDTO pos);
		void UpdatePos(PosDTO pos);
		void DeletePos(string name);
	}
}
