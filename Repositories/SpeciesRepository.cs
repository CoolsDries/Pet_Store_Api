using Microsoft.EntityFrameworkCore;
using Pet_Store_Api.Data;
using Pet_Store_Api.Models;
using Pet_Store_Api.Models.Interfaces;

namespace Pet_Store_Api.Repositories
{
    public class SpeciesRepository : ISpeciesRepository, IDisposable
    {
        private readonly PetStoreContext _context;
        private bool disposedValue;

        public SpeciesRepository(PetStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Species>> GetSpecies()
        {
            return await _context.Species.ToListAsync();
        }

        public async Task<Species?> GetSpeciesById(int id)
        {
            return await _context.Species.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Species>> GetSpeciesByStoreId(int storeId)
        {
            // Getting store by id and including the animals list
            var store = await _context.Stores.Include(s => s.Animals).ThenInclude(a => a.Species).FirstOrDefaultAsync(s => s.Id == storeId);

            // Mapping The animals to theire species and keep unique species
            // Warning checks are not nessecary, they are already check in the controller
            var storeSpecies = store.Animals.Select(a => a.Species);

            return storeSpecies.Distinct();
        }

        public void InsertSpecies(Species species)
        {
            _context.Species.Add(species);
        }

        public void UpdateSpecies(Species species)
        {
            _context.Species.Update(species);
        }

        public void DeleteSpecies(int id)
        {
            _context.Species.Where(s => s.Id == id).ExecuteDelete();
        }

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
