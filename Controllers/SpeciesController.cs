using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

                var speciesDTOs = species.Select(s => new SpeciesDTO(s)).ToList();

                return Ok(speciesDTOs);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/Species/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecies(int id)
        {
            try
            {
                var species = await _speciesRepository.GetSpeciesById(id);

                if (species == null)
                {
                    throw new NotFoundException($"Species with ID {id} not found.");
                }

                return Ok(new SpeciesDTO(species));
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
                var species = await _speciesRepository.GetSpeciesById(id);

                if (species == null)
                {
                    throw new NotFoundException($"Species with ID {id} not found.");
                }

                var animals = await _animalRepository.GetAnimalsBySpieciesId(id);

                if (animals.IsNullOrEmpty())
                {
                    return NotFound("Animals not found."); //code 404
                }

                var animalDTOs = animals.Select(a => new AnimalDTO(a)).ToList();

                return Ok(animalDTOs);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/Species/id
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
        public async Task<ActionResult<Species>> PostSpecies(Species species)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //code 400
            }

            // TODO: check if resource already exists //code 409

            try
            {
                _speciesRepository.InsertSpecies(species);
                await _speciesRepository.Save();

                return Created(); //code 201
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE: api/Species/id
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

        // Check if speices exist, if not throw not found exception.
        private async Task CheckIfSpeciesExist(int id)
        {
            if (await _speciesRepository.GetSpeciesById(id) == null)
            {
                throw new NotFoundException($"Species with ID {id} not found.");
            }
            
        }
    }
}
