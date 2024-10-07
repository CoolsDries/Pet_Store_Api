using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; //Supports LINQ
using Pet_Store_Api.Data;
using Pet_Store_Api.DTOs;
using Pet_Store_Api.Models;
using Pet_Store_Api.Models.Interfaces;

namespace Pet_Store_Api.Repositories
{
    public class StoreRepository : IStoreRepository, IDisposable
    {
        private readonly PetStoreContext _context;
        private bool disposedValue;

        public StoreRepository(PetStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Store>> GetStores()
        {
            // .Include(s => s.Animals)
            return await _context.Stores.ToListAsync();
        }

        public async Task<Store?> GetStoreById(int id)
        {
            // TODO: Using Select and converting directly into DTO
            // So you can controll wich colums are used
            // Question: DTO management in repo or controller (read conflicting things)
            // --> Using models as long as possible, only convert in controller
            return await _context.Stores.Include(s => s.Animals).FirstOrDefaultAsync(s => s.Id == id);
        }

        // Species count = Ammount of animals per species 
        public async Task<IDictionary<string, int>> GetSpeciesAmountFromStores(int[] ids)
        {
            // Get all animals from stores and group by species
            var animals = await _context.Animals
                .Where(a => ids.Contains(a.StoreId))
                .Include(a => a.Species)
                .GroupBy(a => a.Species.Name)
                .ToListAsync();

            // map to dictionary {key: species name, value: amount}
            return animals.ToDictionary(a => a.Key, a => a.Count());
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
