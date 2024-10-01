namespace Pet_Store_Api.Models
{
    public interface IAnimalRepository:IDisposable
    {
        Task<Animal> GetAnimalByID(int id);
        Task<IEnumerable<Animal>> GetAnimals();
        Task<IEnumerable<Animal>> GetAnimalsBySpiecies(int speciesId);
        void InsertAnimal(Animal animal);
        void DeleteAnimal(int id);
        void UpdateAnimal(Animal animal);
        void Save();
    }
}
