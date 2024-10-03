using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pet_Store_Api.DTOs;
using Pet_Store_Api.Models;

namespace Pet_Store_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IAnimalRepository _animalRepository;

        // id in SpeciesController always references speciesId
        public SpeciesController(ISpeciesRepository speciesRepository, IAnimalRepository animalRepository)
        {
            _speciesRepository = speciesRepository;
            _animalRepository = animalRepository;
        }

        // GET: api/Species
        [HttpGet]
        public async Task<IActionResult> GetSpecies()
        {
            try
            {
                var species = await _speciesRepository.GetSpecies();

                if (species.IsNullOrEmpty())
                {
                    return NotFound("Species not found."); //code 404
                }

                var speciesDTOs = species.Select(s => new SpeciesGetDTO(s)).ToList();

                return Ok(speciesDTOs);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/Species/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecies(int id)
        {
            try
            {
                // If species exist, returns that species
                var species = await CheckIfSpeciesExist(id);

                return Ok(new SpeciesGetDTO(species));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/Species/{id}/Animals
        [HttpGet("{id}/Animals")]
        public async Task<IActionResult> GetSpeciesAnimals(int id)
        {
            try
            {
                await CheckIfSpeciesExist(id);

                var animals = await _animalRepository.GetAnimalsBySpieciesId(id);

                if (animals.IsNullOrEmpty())
                {
                    return NotFound("Animals not found."); //code 404
                }

                var animalDTOs = animals.Select(a => new AnimalGetDTO(a)).ToList();

                return Ok(animalDTOs);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/Species/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecies(int id, Species species)
        {
            if (id != species.Id)
            {
                return BadRequest("Id does not match species.Id"); //code 400
            }

            try
            {
                await CheckIfSpeciesExist(id);

                _speciesRepository.UpdateSpecies(species);
                await _speciesRepository.Save();

                return Ok(); //code 200
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: api/Species
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Species>> PostSpecies(SpeciesPostDTO speciesPostDTO)
        {
            // TODO: check if resource already exists //code 409

            try
            {
                Species species = new Species 
                {
                    Name = speciesPostDTO.Name,
                    BasePrice = speciesPostDTO.BasePrice,
                    Description = speciesPostDTO.Description
                };

                _speciesRepository.InsertSpecies(species);
                await _speciesRepository.Save();

                return Created(); //code 201
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE: api/Species/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecies(int id)
        {
            try
            {
                await CheckIfSpeciesExist(id);

                _speciesRepository.DeleteSpecies(id);
                await _speciesRepository.Save();

                return Ok(); //code 200
            }
            catch (Exception)
            {
                throw;
            }
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
