using Microsoft.AspNetCore.Mvc;

namespace Pet_Store_Api.Models.Interfaces
{
    public interface IStoreRepository : IDisposable
    {
        Task<Store?> GetStoreById(int id);
        Task<IEnumerable<Store>> GetStores();
        //Task<IDictionary<string, int>> GetStoreStock(int id);
        Task<IDictionary<string, int>> GetStoresStock(int[] ids);
        void InsertStore(Store store);
        void DeleteStore(int id);
        void UpdateStore(Store store);
        Task Save();
    }
}
