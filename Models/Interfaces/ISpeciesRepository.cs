namespace Pet_Store_Api.Models.Interfaces
{
    public interface ISpeciesRepository : IDisposable
    {
        Task<IEnumerable<Species>> GetSpecies();
        Task<Species?> GetSpeciesById(int id);
        void InsertSpecies(Species species);
        void DeleteSpecies(int id);
        void UpdateSpecies(Species species);
        Task Save();

        // Not using methods unitl neccesary
        //Task<IEnumerable<Species>> GetSpeciesByStoreId(int storeId);
    }
}
