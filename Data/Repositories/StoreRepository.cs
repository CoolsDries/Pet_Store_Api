using Microsoft.EntityFrameworkCore; //Supports LINQ
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

        public async Task<Store?> GetStoreById(int id)
        {
            // TODO: Using Select and converting directly into DTO
            // So you can controll wich colums are used
            // Question: DTO management in repo or controller (read conflicting things)
            //StoreGetDTO storeGetDTO = await _context.Stores
            //    .Select(s => new StoreGetDTO
            //    {
            //        Id = s.Id,
            //        Name = s.Name,
            //        Location = s.Location,
            //    })
            //    .FirstOrDefaultAsync(s => s.Id == id);

            return await _context.Stores.Include(s => s.Animals).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Store>> GetStores()
        {
            // .Include(s => s.Animals)
            return await _context.Stores.ToListAsync();
        }

        // Done in Repository and not in Domain model, because of Domain limitations.
        public async Task<IDictionary<Species, int>> GetStoreStock(int id)
        {
            // Get all animals from store and group by species and then include species object
            var animals = await _context.Animals
                .Where(a => a.StoreId == id)
                .Include(a => a.Species)
                .GroupBy(a => a.Species)
                .ToListAsync();

            // map to dictionary
            var storeStock = animals.ToDictionary(a => a.Key, a => a.Count());

            return storeStock;
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
