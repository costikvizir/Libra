using LibraBll.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraBll.Abstractions.Repositories
{
    public interface IPosRepository
    {
        Task<PosDTO> GetPosByIdAsync(int id);

        Task<List<PosDTO>> GetAllPosAsync();

        Task<PosDTO> AddPosAsync(PosDTO pos);

        void UpdatePos(PosDTO pos);

        void DeletePos(string name);
    }
}