namespace Pet_Store_Api.Models
{
    public interface IStoreRepository:IDisposable
    {
        Task<Store?> GetStoreByID(int id);
        Task<IEnumerable<Store>> GetStores();
        void InsertStore(Store store);
        void DeleteStore(int id);
        void UpdateStore(Store store);
        Task Save();
    }
}
