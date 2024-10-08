using Humanizer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NuGet.Packaging;
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

            List<String> storeNames = []; // To create, the chart name of al combined store names
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
            var speciesAmountForStores_DTO = new Chart_SpeciesAmountForStores_DTO
            {
                Name = String.Join(", ", [.. storeNames]),
                SpeciesAmounts = speciesAmount

            };

            return speciesAmountForStores_DTO;
        }

        public async Task<Chart_CompareSpeciesAmountForStores_DTO> GetCompareSpeciesAmountForStores(int[] ids)
        {
            if (ids.IsNullOrEmpty())
            {
                throw new NotFoundException("Stores not found."); //code 404
            }
       
            List<String> storeNames = []; // To create, the chart name of al combined store names
            List<Chart_SpeciesAmountForStores_DTO> speciesAmountForStores_DTOs = [];
            // Iterate each id, to get SpeciesAmount for each store seperatly
            foreach (var id in ids)
            {
                var store = await CheckIfStoreExist(id);
                storeNames.Add(store.Name);

                var speciesAmount = await _storeRepository.GetSpeciesAmountFromStores([id]);

                if (speciesAmount.IsNullOrEmpty())
                {
                    throw new NotFoundException("No species found for given store ids"); //code 404
                }

                speciesAmountForStores_DTOs.Add(new Chart_SpeciesAmountForStores_DTO
                {
                    Name = store.Name,
                    SpeciesAmounts = speciesAmount

                });
            }

            // Initialize return DTO
            var compareSpeciesAmountForStores_DTO = new Chart_CompareSpeciesAmountForStores_DTO
            {
                Name = String.Join(", ", [.. storeNames]),
                Stores = speciesAmountForStores_DTOs.ToArray()

            };

            return compareSpeciesAmountForStores_DTO;
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
