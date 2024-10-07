using Microsoft.IdentityModel.Tokens;
using Pet_Store_Api.Models;
using Pet_Store_Api.Models.Interfaces;

namespace Pet_Store_Api.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<IEnumerable<Store>> GetStores()
        {
            var stores = await _storeRepository.GetStores();

            if (stores.IsNullOrEmpty())
            {
                throw new NotFoundException("Stores not found."); //code 404
            }

            return stores;
        }

        public async Task<Store?> GetStoreById(int id)
        {
            var store = await CheckIfStoreExist(id);

            return store;
        }

        public async Task InsertStore(Store store)
        {
            _storeRepository.InsertStore(store);
            await _storeRepository.Save();
        }

        public async Task DeleteStore(int id)
        {
            await CheckIfStoreExist(id);

            _storeRepository.DeleteStore(id);
            await _storeRepository.Save();
        }

        public async Task UpdateStore(int id, Store store)
        {
            if (id != store.Id)
            {
                throw new BadRequestException("Id does not match store.Id"); //code 400
            }

            await CheckIfStoreExist(id);

            _storeRepository.UpdateStore(store);
            await _storeRepository.Save();
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
