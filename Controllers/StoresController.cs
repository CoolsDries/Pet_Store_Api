using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pet_Store_Api.DTOs;
using Pet_Store_Api.Models;

namespace Pet_Store_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly ISpeciesRepository _speciesRepository;

        // id in StoreController always references storeId
        public StoresController(IStoreRepository storeRepository, IAnimalRepository animalRepository, ISpeciesRepository speciesRepository)
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

                var storeGetDTOs = stores.Select(s => new StoreGetDTO(s)).ToList();

                return Ok(storeGetDTOs);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/Stores/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStore(int id)
        {
            try
            {
                var store = await CheckIfStoreExist(id);

                return Ok(new StoreGetDTO(store));
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
                await CheckIfStoreExist(id);

                // Not via store.include() because we will need store info and animals seperatly.
                var animals = await _animalRepository.GetAnimalsByStoreId(id);

                if (animals.IsNullOrEmpty())
                {
                    return NotFound("Animals not found."); //code 404
                }

                var animalsDTOs = animals.Select(a => new AnimalGetDTO(a)).ToList();

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
                await CheckIfStoreExist(id);

                var species = await _speciesRepository.GetSpeciesByStoreId(id);

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

        // GET: api/Stores/{id}/Species/{speciesId}/Animals
        [HttpGet("{id}/Species/{speciesId}/Animals")]
        public async Task<IActionResult> GetStoreAnimalsBySpecies(int id, int speciesId)
        {
            try
            {
                await CheckIfStoreExist(id);

                await CheckIfSpeciesExist(id);

                // TODO: Check if store contains species

                var animals = await _animalRepository.GetAnimalsByStoreIdAndSpieciesId(id, speciesId);

                if (animals.IsNullOrEmpty())
                {
                    return NotFound("Animals not found."); //code 404
                }

                var animalsDTOs = animals.Select(a => new AnimalGetDTO(a)).ToList();

                return Ok(animalsDTOs);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/{id}/Stock
        // TODO: Swagger documentation
        [HttpGet("{id}/Stock")]
        public async Task<IActionResult> GetStoreStock(int id)
        {
            // Question: What is the best way to implement stock info?
            try
            {
                var store = await CheckIfStoreExist(id);

                var storeStock =  await _storeRepository.GetStoreStock(id);

                if (!storeStock.Any())
                {
                    return NotFound("No stock found for this store.");
                }

                // Initialize return DTO
                StockDTO stockDTO = new StockDTO
                {
                    storeGetDTO = new StoreGetDTO(store),
                    SpeciesStocks = []

                };

                // Add foreach species a speciesstockDTO to the stockDTO
                foreach (var stock in storeStock)
                {
                    SpeciesStockDTO speciesStockDTO = new SpeciesStockDTO
                    {
                        speciesGetDTO = new SpeciesGetDTO(stock.Key),
                        AnimalsAmount = stock.Value,
                    };
                    stockDTO.SpeciesStocks.Add(speciesStockDTO);
                }

                return Ok(stockDTO);
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        // PUT: api/Stores/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStore(int id, Store store)
        {
            try
            {
                if (id != store.Id)
                {
                    return BadRequest("Id does not match store.Id"); //code 400
                }

                await CheckIfStoreExist(id);

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
        public async Task<ActionResult<Store>> PostStore(StorePostDTO storePostDTO)
        {
            // TODO: check if resource already exists //code 409

            try
            {
                Store store = new Store
                {
                    Name = storePostDTO.Name,
                    Location = storePostDTO.Location
                };

                _storeRepository.InsertStore(store);
                await _storeRepository.Save();

                return Created(); //code 201
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE: api/Stores/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            try
            {
                await CheckIfStoreExist(id);

                _storeRepository.DeleteStore(id);
                await _storeRepository.Save();

                return Ok(); //code 200
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Check if store exist and returns store, if not throw not found exception.
        private async Task<Store> CheckIfStoreExist(int id)
        {
            var store = await _storeRepository.GetStoreById(id);

            if (store == null)
            {
                throw new NotFoundException($"Store with ID {id} not found.");
            }

            return store;
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
