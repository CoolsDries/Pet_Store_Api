using Microsoft.AspNetCore.Mvc;
using Pet_Store_Api.DTOs;

namespace Pet_Store_Api.Models.Interfaces
{
    public interface IAnimalService
    {
        Task<IEnumerable<Animal>> GetAnimals();
        Task<Animal?> GetAnimalById(int id);
        Task InsertAnimal(Animal animal);
        Task DeleteAnimal(int id);
        Task UpdateAnimal(int id, Animal animal);
    }
}
