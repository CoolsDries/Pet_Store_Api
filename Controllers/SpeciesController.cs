using Microsoft.AspNetCore.Mvc;
using Pet_Store_Api.DTOs;
using Pet_Store_Api.Models;
using Pet_Store_Api.Models.Interfaces;

namespace Pet_Store_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly ISpeciesService _speciesService;

        // id in SpeciesController always references speciesId
        public SpeciesController(ISpeciesService speciesService)
        {
            _speciesService = speciesService;
        }

        // GET: api/Species
        [HttpGet]
        public async Task<IActionResult> GetSpecies()
        {
            try
            {
                var species = await _speciesService.GetSpecies();

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
                var species = await _speciesService.GetSpeciesById(id);

                return Ok(new SpeciesGetDTO(species));
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
            try
            {
                var species = SpeciesDTOMapper.SpeciesPostDTO_to_Species(speciesPostDTO);

                await _speciesService.InsertSpecies(species);

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
                await _speciesService.DeleteSpecies(id);

                return Ok(); //code 200
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
            try
            {
                await _speciesService.UpdateSpecies(id, species);

                return Ok(); //code 200
            }
            catch (Exception)
            {
                throw;
            }
        }

        //// GET: api/Species/{id}/Animals
        //[HttpGet("{id}/Animals")]
        //public async Task<IActionResult> GetSpeciesAnimals(int id)
        //{
        //    try
        //    {
        //        await CheckIfSpeciesExist(id);

        //        var animals = await _animalRepository.GetAnimalsBySpieciesId(id);

        //        if (animals.IsNullOrEmpty())
        //        {
        //            return NotFound("Animals not found."); //code 404
        //        }

        //        var animalDTOs = animals.Select(a => new AnimalGetDTO(a)).ToList();

        //        return Ok(animalDTOs);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
