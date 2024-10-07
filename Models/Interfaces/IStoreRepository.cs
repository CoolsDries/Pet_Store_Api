using Microsoft.AspNetCore.Mvc;

namespace Pet_Store_Api.Models.Interfaces
{
    public interface IStoreRepository : IDisposable
    {
        Task<IEnumerable<Store>> GetStores();
        Task<Store?> GetStoreById(int id);
        Task<IDictionary<string, int>> GetSpeciesAmountFromStores(int[] ids);
        void InsertStore(Store store);
        void DeleteStore(int id);
        void UpdateStore(Store store);
        Task Save();
    }
}
