using Pet_Store_Api.Models;

namespace Pet_Store_Api.Data.Repositories
{
    public class AnimalRepository : IAnimalRepository, IDisposable
    {
        private bool disposedValue;

        public void DeleteAnimal(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Animal> GetAnimalByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Animal>> GetAnimals()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Animal>> GetAnimalsBySpiecies(int speciesId)
        {
            throw new NotImplementedException();
        }

        public void InsertAnimal(Animal animal)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateAnimal(Animal animal)
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
        // ~AnimalRepository()
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
