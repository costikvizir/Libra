using LibraBll.Common;
using LibraBll.DTOs.Pos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraBll.Abstractions.Repositories
{
    public interface IPosRepository
    {
        Task<PosGetDTO> GetPosByIdAsync(int id);

        Task<List<PosGetDTO>> GetAllPosAsync();

        Task<PosPostDTO> AddPosAsync(PosPostDTO pos);

        void UpdatePos(PosPostDTO pos);

        void DeletePos(string name);
    }
}