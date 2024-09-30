using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pet_Store_Api.Models;

namespace Pet_Store_Api.Data.Repositories
{
    public class StoreRepository : IStoreRepository, IDisposable
    {
        private readonly PetStoreContext _context;
        private bool disposedValue;

        public StoreRepository(PetStoreContext context) 
        {
            _context = context;
        }

        public void DeleteStore(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Store> GetStoreByID(int id)
        {
            return await _context.Stores.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<IEnumerable<Store>> GetStores()
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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~StoreRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
