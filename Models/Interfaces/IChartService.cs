using Pet_Store_Api.DTOs;

namespace Pet_Store_Api.Models.Interfaces
{
    public interface IChartService
    {
        Task<Chart_SpeciesAmountForStores_DTO> GetSpeciesAmountForStores(int[] ids);
    }
}
