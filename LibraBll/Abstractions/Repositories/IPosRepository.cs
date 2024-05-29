using LibraBll.Common.DataTableModels;
using LibraBll.DTOs.Dropdown;
using LibraBll.DTOs.Pos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraBll.Abstractions.Repositories
{
    public interface IPosRepository
    {
        Task<PosGetDTO> GetPosByIdAsync(int id);

        Task<List<PosGetDTO>> GetAllPosAsync(DataTablesParameters parameters, CancellationToken cancellationToken);
        Task<List<PosGetDTO>> GetAllPosAsync(DataTablesParameters parameters, string name, string brand, string fullAddress, CancellationToken cancellationToken);

        Task<PosPostDTO> AddPosAsync(PosPostDTO pos);

        Task UpdatePos(PosEditDTO pos);

        void DeletePos(int id);

        Task<IEnumerable<CityDTO>> GetCityList();

        Task<List<ConnectionTypeDTO>> GetConnectionTypeList();

        Task<List<PosWeekDayDTO>> GetPosClosingDays(int posId);
    }
}