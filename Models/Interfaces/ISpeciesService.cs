namespace Pet_Store_Api.Models.Interfaces
{
    public interface ISpeciesService
    {
        Task<IEnumerable<Species>> GetSpecies();
        Task<Species?> GetSpeciesById(int id);
        Task InsertSpecies(Species species);
        Task DeleteSpecies(int id);
        Task UpdateSpecies(int id, Species species);
    }
}
