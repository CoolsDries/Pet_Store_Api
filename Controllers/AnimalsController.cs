using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pet_Store_Api.DTOs;
using Pet_Store_Api.Models;

namespace Pet_Store_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalsController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<IActionResult> GetAnimals()
        {
            try
            {
                var animals = await _animalRepository.GetAnimals();

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

        // GET: api/Animals/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimal(int id)
        {
            try
            {
                var animal = await _animalRepository.GetAnimalById(id);

                if (animal == null)
                {
                    return NotFound("Animal not found."); //code 404
                }

                return Ok(new AnimalDTO(animal));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/Animals/id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            if (id != animal.Id)
            {
                return BadRequest("Id does not match animal.Id"); //code 400
            }

            if (await _animalRepository.GetAnimalById(id) == null)
            {
                return NotFound("Animal not found."); //code 404
            }

            try
            {
                _animalRepository.UpdateAnimal(animal);
                await _animalRepository.Save();

                return Ok(); //code 200
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: api/Animals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //code 400
            }

            // TODO: check if resource already exists //code 409

            try
            {
                _animalRepository.UpdateAnimal(animal);
                await _animalRepository.Save();

                return Created(); //code 201
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE: api/Animals/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            if (await _animalRepository.GetAnimalById(id) == null)
            {
                return NotFound("Store not found."); //code 404
            }

            try
            {
                _animalRepository.DeleteAnimal(id);
                await _animalRepository.Save();

                return Ok(); //code 200
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
