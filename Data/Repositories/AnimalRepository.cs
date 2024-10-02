using Microsoft.EntityFrameworkCore;
using Pet_Store_Api.Models;

namespace Pet_Store_Api.Data.Repositories
{
    public class AnimalRepository : IAnimalRepository, IDisposable
    {
        private readonly PetStoreContext _context;
        private bool disposedValue;

        public AnimalRepository(PetStoreContext context)
        {
            _context = context;
        }
        public async Task<Animal?> GetAnimalById(int id)
        {
            return await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Animal>> GetAnimals()
        {
            return await _context.Animals.ToListAsync();
        }

        public async Task<IEnumerable<Animal>> GetAnimalsByStoreId(int storeId)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(s => s.Id == storeId);
            return await _context.Animals.Where(a => a.Store == store).ToListAsync();
        }

        public async Task<IEnumerable<Animal>> GetAnimalsBySpieciesId(int speciesId)
        {
            var species = await _context.Species.FirstOrDefaultAsync(s => s.Id == speciesId);
            return await _context.Animals.Where(a => a.Species == species).ToListAsync();
        }

        public async Task<IEnumerable<Animal>> GetAnimalsByStoreIdAndSpieciesId(int storeId, int speciesId)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(s => s.Id == storeId);
            var species = await _context.Species.FirstOrDefaultAsync(s => s.Id == speciesId);

            return await _context.Animals.Where(a => a.Store == store && a.Species == species).ToListAsync();
        }

        public void InsertAnimal(Animal animal)
        {
            _context.Animals.Add(animal);   
        }

        public void UpdateAnimal(Animal animal)
        {
            _context.Animals.Update(animal);
        }

        public void DeleteAnimal(int id)
        {
            _context.Animals.Where(a => a.Id == id).ExecuteDelete();
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
