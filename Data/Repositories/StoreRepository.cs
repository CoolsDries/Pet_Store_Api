using Pet_Store_Api.Models;

namespace Pet_Store_Api.Data.Repositories
{
    public class StoreRepository : IStoreRepository, IDisposable
    {
        private readonly PetStoreContext _context;

        public StoreRepository(PetStoreContext context) 
        {
            _context = context;
        }

        public void DeleteStore(int id)
        {
            throw new NotImplementedException();
        }

        public Store GetStoreByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Store> GetStores()
        {
            throw new NotImplementedException();
        }

        public void InsertStore(Store store)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateStore(Store store)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
