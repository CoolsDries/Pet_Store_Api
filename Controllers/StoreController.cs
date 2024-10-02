using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pet_Store_Api.DTOs;
using Pet_Store_Api.Models;

namespace Pet_Store_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly ISpeciesRepository _speciesRepository;
        
        public StoreController(IStoreRepository storeRepository, IAnimalRepository animalRepository, ISpeciesRepository speciesRepository)
        {
            _storeRepository = storeRepository;
            _animalRepository = animalRepository;
            _speciesRepository = speciesRepository;
        }

        // GET: api/Stores
        [HttpGet]
        public async Task<IActionResult> GetStores()
        {
            try
            {
                var stores = await _storeRepository.GetStores();

                if (stores.IsNullOrEmpty())
                {
                    return NotFound("Stores not found."); //code 404
                }

                var storeDTOs = stores.Select(s => new StoreDTO(s)).ToList();

                return Ok(storeDTOs);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/Stores/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStore(int id)
        {
            try
            {
                var store = await _storeRepository.GetStoreByID(id);

                if (store == null)
                {
                    return NotFound("Store not found."); //code 404
                }

                return Ok(new StoreDTO(store));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/Stores/{id}/Animals
        [HttpGet("{id}/Animals")]
        public async Task<IActionResult> GetStoreAnimals(int id)
        {
            try
            {
                var store = await _storeRepository.GetStoreByID(id);

                if (store == null)
                {
                    return NotFound("Store not found."); //code 404
                }

                // Not via store.include() because we will need store info and animals seperatly.
                var animals = await _animalRepository.GetAnimalsByStoreId(id);

                if (animals.IsNullOrEmpty())
                {
                    return NotFound("Animals not found."); //code 404
                }

                var animalsDTOs = animals.Select(a => new AnimalDTO(a)).ToList();

                return Ok(animalsDTOs);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/Stores/{id}/Species
        [HttpGet("{id}/Species")]
        public async Task<IActionResult> GetStoreSpecies(int id)
        {
            try
            {
                var store = await _storeRepository.GetStoreByID(id);

                if (store == null)
                {
                    return NotFound("Store not found."); //code 404
                }

                var species = await _speciesRepository.GetSpeciesByStoreId(id);

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

        // GET: api/Stores/{id}/Species/{speciesId}/Animals
        [HttpGet("{id}/Species/{speciesId}/Animals")]
        public async Task<IActionResult> GetStoreAnimalsBySpecies(int id, int speciesId)
        {
            try
            {
                var store = await _storeRepository.GetStoreByID(id);

                if (store == null)
                {
                    return NotFound("Store not found."); //code 404
                }

                var species = await _speciesRepository.GetSpeciesById(speciesId);

                if (species == null)
                {
                    return NotFound("Species not found."); //code 404
                }

                // TODO: Check if store contains species

                var animals = await _animalRepository.GetAnimalsByStoreIdAndSpieciesId(id, speciesId);

                if (animals.IsNullOrEmpty())
                {
                    return NotFound("Animals not found."); //code 404
                }

                var animalsDTOs = animals.Select(a => new AnimalDTO(a)).ToList();

                return Ok(animalsDTOs);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/Stores/id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStore(int id, Store store)
        {
            if (id != store.Id)
            {
                return BadRequest("Id does not match store.Id"); //code 400
            }

            if (await _storeRepository.GetStoreByID(id) == null)
            {
                return NotFound("Store not found."); //code 404
            }

            try
            {               
                _storeRepository.UpdateStore(store);
                await _storeRepository.Save();

                return Ok(); //code 200
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Store>> PostStore(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //code 400
            }

            // TODO: check if resource already exists //code 409

            try
            {
                _storeRepository.InsertStore(store);
                await _storeRepository.Save();

                return Created(); //code 201
            }
            catch (Exception)
            {
                throw;
            }
        }

            // DELETE: api/Stores/id
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            if (await _storeRepository.GetStoreByID(id) == null)
            {
                return NotFound("Store not found."); //code 404
            }

            try
            {
                _storeRepository.DeleteStore(id);
                await _storeRepository.Save();

                return Ok(); //code 200
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
