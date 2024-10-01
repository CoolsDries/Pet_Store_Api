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

        public async Task<Store?> GetStoreByID(int id)
        {
            return await _context.Stores.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Store>> GetStores()
        {
            // .Include(s => s.Animals)
            return await _context.Stores.ToListAsync();
        }

        // Insert, Delete and Update, don't need to be async.
        // Changes to the database only occur when SaveChanges is called.
        public void InsertStore(Store store)
        {
            _context.Stores.Add(store);
        }

        public void DeleteStore(int id)
        {
             _context.Stores.Where(s => s.Id == id).ExecuteDelete();
        }

        public void UpdateStore(Store store)
        {
            _context.Stores.Update(store);
        }

        // Best practices: Avoid async void whenever possible
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        // Disposing of _context when repository is no longer needed
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_context != null)
                    {
                        _context.Dispose();
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
