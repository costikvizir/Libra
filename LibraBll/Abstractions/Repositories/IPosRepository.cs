using LibraBll.DTOs.Dropdown;
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

        Task UpdatePos(PosEditDTO pos);

        void DeletePos(int id);

        List<CityDTO> GetCityList();

        List<ConnectionTypeDTO> GetConnectionTypeList();
    }
}