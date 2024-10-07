using Microsoft.IdentityModel.Tokens;
using Pet_Store_Api.DTOs;
using Pet_Store_Api.Models;
using Pet_Store_Api.Models.Interfaces;

namespace Pet_Store_Api.Services
{
    public class ChartService : IChartService
    {
        private readonly IStoreRepository _storeRepository;

        public ChartService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<Chart_SpeciesAmountForStores_DTO> GetSpeciesAmountForStores(int[] ids)
        {

            if (ids.IsNullOrEmpty())
            {
                throw new NotFoundException("Stores not found."); //code 404
            }

            List<String> storeNames = [];
            foreach (var id in ids)
            {
                var store = await CheckIfStoreExist(id);
                storeNames.Add(store.Name);
            }

            var speciesAmount = await _storeRepository.GetSpeciesAmountFromStores(ids);
            if (speciesAmount.IsNullOrEmpty())
            {
                throw new NotFoundException("No species found for given store ids"); //code 404
            }

            // Initialize return DTO
            Chart_SpeciesAmountForStores_DTO dto = new Chart_SpeciesAmountForStores_DTO
            {
                StoreName = String.Join(", ", [.. storeNames]),
                SpeciesAmount = speciesAmount

            };

            return dto;
        }

        // Check if store exist and returns store, if not throw not found exception.
        private async Task<Store> CheckIfStoreExist(int id)
        {
            var store = await _storeRepository.GetStoreById(id);

            if (store == null)
            {
                throw new NotFoundException($"Store with ID {id} not found.");
            }

            return store;
        }
    }
}
