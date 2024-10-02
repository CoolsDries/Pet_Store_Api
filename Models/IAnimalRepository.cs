namespace Pet_Store_Api.Models
{
    public interface IAnimalRepository:IDisposable
    {
        Task<Animal?> GetAnimalById(int id);
        Task<IEnumerable<Animal>> GetAnimals();
        Task<IEnumerable<Animal>> GetAnimalsByStoreId(int storeId);
        Task<IEnumerable<Animal>> GetAnimalsBySpieciesId(int speciesId);
        Task<IEnumerable<Animal>> GetAnimalsByStoreIdAndSpieciesId(int storeId, int speciesId);
        void InsertAnimal(Animal animal);
        void DeleteAnimal(int id);
        void UpdateAnimal(Animal animal);
        Task Save();
    }
}
