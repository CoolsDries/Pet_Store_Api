using Pet_Store_Api.DTOs;

namespace Pet_Store_Api.Models.Interfaces
{
    public interface IAnimalRepository : IDisposable
    {
        Task<IEnumerable<Animal>> GetAnimals();
        Task<Animal?> GetAnimalById(int id);
        void InsertAnimal(Animal animal);
        void DeleteAnimal(int id);
        void UpdateAnimal(Animal animal);
        Task Save();

        // Not using methods unitl neccesary
        //Task<IEnumerable<Animal>> GetAnimalsByStoreId(int storeId);
        //Task<IEnumerable<Animal>> GetAnimalsBySpieciesId(int speciesId);
        //Task<IEnumerable<Animal>> GetAnimalsByStoreIdAndSpieciesId(int storeId, int speciesId);
    }
}