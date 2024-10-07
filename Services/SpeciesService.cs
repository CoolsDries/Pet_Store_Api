using Microsoft.IdentityModel.Tokens;
using Pet_Store_Api.DTOs;
using Pet_Store_Api.Models;
using Pet_Store_Api.Models.Interfaces;

namespace Pet_Store_Api.Services
{
    public class SpeciesService : ISpeciesService
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IAnimalRepository _animalRepository;

        public SpeciesService(ISpeciesRepository speciesRepository, IAnimalRepository animalRepository)
        {
            _speciesRepository = speciesRepository;
            _animalRepository = animalRepository;
        }

        public async Task<IEnumerable<Species>> GetSpecies()
        {
            var species = await _speciesRepository.GetSpecies();

            if (species.IsNullOrEmpty())
            {
                throw new NotFoundException("Species not found."); //code 404
            }

            return species;
        }

        public async Task<Species?> GetSpeciesById(int id)
        {
            var species = await CheckIfSpeciesExist(id);

            return species;
        }

        public async Task InsertSpecies(Species species)
        {
            _speciesRepository.InsertSpecies(species);
            await _speciesRepository.Save();
        }

        public async Task DeleteSpecies(int id)
        {
            await CheckIfSpeciesExist(id);

            _speciesRepository.DeleteSpecies(id);
            await _speciesRepository.Save();
        }

        public async Task UpdateSpecies(int id, Species species)
        {
            if (id != species.Id)
            {
                throw new BadRequestException("Id does not match species.Id"); //code 400
            }

            await CheckIfSpeciesExist(id);

            _speciesRepository.UpdateSpecies(species);
            await _speciesRepository.Save();
        }
           

        // Check if speices exist and returns species, if not throw not found exception.
        private async Task<Species> CheckIfSpeciesExist(int id)
        {
            var species = await _speciesRepository.GetSpeciesById(id);

            if (species == null)
            {
                throw new NotFoundException($"Species with ID {id} not found.");
            }

            return species;
        }
    }
}
