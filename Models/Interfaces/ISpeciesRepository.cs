namespace Pet_Store_Api.Models.Interfaces
{
    public interface ISpeciesRepository : IDisposable
    {
        Task<Species?> GetSpeciesById(int id);
        Task<IEnumerable<Species>> GetSpeciesByStoreId(int storeId);
        Task<IEnumerable<Species>> GetSpecies();
        void InsertSpecies(Species species);
        void DeleteSpecies(int id);
        void UpdateSpecies(Species species);
        Task Save();
    }
}
