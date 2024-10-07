using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pet_Store_Api.DTOs;
using Pet_Store_Api.Models;
using Pet_Store_Api.Models.Interfaces;
using Pet_Store_Api.Repositories;

// TODO, Question: Add and implement a Service Layer
// Controller -> Service -> Repository
// What needs to be where? (Null, empty checks (everywhere?), other errors)?
// All busines logica into Services

namespace Pet_Store_Api.Controllers
{
    [Route("api/[controller]")]
    // ApiController will automatically apply model validation rules
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        // Error handeling in Service
        // id in AnimalsController always references animalId
        public AnimalsController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        // TODO: order queries
        // TODO: swagger documentation

        // GET: api/Animals
        [HttpGet]
        public async Task<IActionResult> GetAnimals()
        {
            try
            {
                var animals = await _animalService.GetAnimals();

                // Convert to DTO to control outgoing data
                var animalDTOs = animals.Select(a => new AnimalGetDTO(a)).ToList();

                return Ok(animalDTOs);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/Animals/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimal(int id)
        {
            try
            {
                var animal = await _animalService.GetAnimalById(id);
                
                return Ok(new AnimalGetDTO(animal));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: api/Animals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(AnimalPostDTO animalPostDTO)
        {
            try
            {
                var animal = AnimalDTOMapper.AnimalPostDTO_to_Animal(animalPostDTO);

                await _animalService.InsertAnimal(animal);

                return Created(); //code 201
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE: api/Animals/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            try
            {
                await _animalService.DeleteAnimal(id);

                return Ok(); //code 200
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/Animals/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            try
            {
                await _animalService.UpdateAnimal(id, animal);

                return Ok(); //code 200
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
