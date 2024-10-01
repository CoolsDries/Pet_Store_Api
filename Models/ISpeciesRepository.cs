namespace Pet_Store_Api.Models
{
    public interface ISpeciesRepository:IDisposable
    {
        Task<Species> GetSpeciesByID(int id);
        Task<IEnumerable<Species>> GetSpecies();
        void InsertSpecies(Species species);
        void DeleteSpecies(int id);
        void UpdateSpecies(Species species);
        void Save();
    }
}
