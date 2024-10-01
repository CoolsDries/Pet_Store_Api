using Pet_Store_Api.Models;

namespace Pet_Store_Api.Data.Repositories
{
    public class SpeciesRepository : ISpeciesRepository, IDisposable
    {
        private bool disposedValue;

        public void DeleteSpecies(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Species>> GetSpecies()
        {
            throw new NotImplementedException();
        }

        public Task<Species> GetSpeciesByID(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertSpecies(Species species)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateSpecies(Species species)
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
        // ~SpeciesRepository()
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
