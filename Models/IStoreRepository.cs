namespace Pet_Store_Api.Models
{
    public interface IStoreRepository:IDisposable
    {
        Store GetStoreByID(int id);
        IEnumerable<Store> GetStores();
        void InsertStore(Store store);
        void DeleteStore(int id);
        void UpdateStore(Store store);
        void Save();
    }
}
