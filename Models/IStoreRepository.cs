namespace Pet_Store_Api.Models
{
    public interface IStoreRepository:IDisposable
    {
        Task<Store?> GetStoreById(int id);
        Task<IEnumerable<Store>> GetStores();
        Task<IDictionary<Species, int>> GetStoreStock(int id);
        void InsertStore(Store store);
        void DeleteStore(int id);
        void UpdateStore(Store store);
        Task Save();
    }
}
