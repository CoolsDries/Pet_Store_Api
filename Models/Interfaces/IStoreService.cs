namespace Pet_Store_Api.Models.Interfaces
{
    public interface IStoreService
    {
        Task<IEnumerable<Store>> GetStores();
        Task<Store?> GetStoreById(int id);
        Task InsertStore(Store store);
        Task DeleteStore(int id);
        Task UpdateStore(int id, Store store);
    }
}
