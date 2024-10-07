using Microsoft.IdentityModel.Tokens;
using Pet_Store_Api.Models;
using Pet_Store_Api.Models.Interfaces;

namespace Pet_Store_Api.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly ISpeciesRepository _speciesRepository;

        public AnimalService(IAnimalRepository animalRepository, IStoreRepository storeRepository, ISpeciesRepository speciesRepository)
        {
            _animalRepository = animalRepository;
            _storeRepository = storeRepository;
            _speciesRepository = speciesRepository;
        }

        public async Task<IEnumerable<Animal>> GetAnimals()
        {

            var animals = await _animalRepository.GetAnimals();

            if (animals.IsNullOrEmpty())
            {
                throw new NotFoundException("Animals not found."); //code 404
            }

            return animals;
        }

        public async Task<Animal?> GetAnimalById(int id)
        {
            var animal = await CheckIfAnimalExist(id);

            return animal;
        }

        public async Task InsertAnimal(Animal animal)
        {
            var species = await _speciesRepository.GetSpeciesById(animal.SpeciesId);
            var store = await _storeRepository.GetStoreById(animal.StoreId);

            if (species == null)
            {
                throw new NotFoundException($"Species with ID {animal.SpeciesId} not found.");
            }
            if (store == null)
            {
                throw new NotFoundException($"Store with ID {animal.StoreId} not found.");
            }

            animal.Species = species;
            animal.Store = store;

            _animalRepository.InsertAnimal(animal);
            await _animalRepository.Save();
        }

        public async Task DeleteAnimal(int id)
        {
            await CheckIfAnimalExist(id);

            _animalRepository.DeleteAnimal(id);
            await _animalRepository.Save();
        }

        public async Task UpdateAnimal(int id, Animal animal)
        {
            if (id != animal.Id)
            {
                throw new BadRequestException("Id does not match animal.Id"); //code 400
            }

            await CheckIfAnimalExist(id);

            _animalRepository.UpdateAnimal(animal);
            await _animalRepository.Save();
        }

        // Check if animal exist and returns animal, if not throw not found exception.
        private async Task<Animal> CheckIfAnimalExist(int id)
        {
            var animal = await _animalRepository.GetAnimalById(id);

            if (animal == null)
            {
                throw new NotFoundException($"Animal with ID {id} not found.");
            }

            return animal;
        }
    }
}
