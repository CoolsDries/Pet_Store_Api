using Microsoft.AspNetCore.Mvc;
using Pet_Store_Api.DTOs;
using Pet_Store_Api.Models;
using Pet_Store_Api.Models.Interfaces;

namespace Pet_Store_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;

        // id in StoreController always references storeId
        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        // GET: api/Stores
        [HttpGet]
        public async Task<IActionResult> GetStores()
        {
            try
            {
                var stores = await _storeService.GetStores();

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
                var store = await _storeService.GetStoreById(id);

                return Ok(new StoreGetDTO(store));
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
            try
            {
                var store = StoreDTOMapper.StorePostDTO_to_Store(storePostDTO);

                await _storeService.InsertStore(store);

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
                await _storeService.DeleteStore(id);

                return Ok(); //code 200
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
                await _storeService.UpdateStore(id, store);

                return Ok(); //code 200
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Not using methods unitl neccesary
        //// GET: api/{id}/Stock
        //// TODO: Swagger documentation
        //[HttpGet("{id}/Stock")]
        //public async Task<IActionResult> GetStoreStock(int id)
        //{
        //    // Question: What is the best way to implement stock info?
        //    try
        //    {
        //        var store = await CheckIfStoreExist(id);

        //        var storeStock =  await _storeRepository.GetStoreStock(id);

        //        if (!storeStock.Any())
        //        {
        //            return NotFound("No stock found for this store.");
        //        }

        //        // Initialize return DTO
        //        StockDTO stockDTO = new StockDTO
        //        {
        //            StoreName = store.Name,
        //            SpeciesStocks = []

        //        };

        //        // Add foreach species a speciesstockDTO to the stockDTO
        //        foreach (var stock in storeStock)
        //        {
        //            SpeciesStockDTO speciesStockDTO = new SpeciesStockDTO
        //            {
        //                SpeciesName = stock.Key,
        //                AnimalsAmount = stock.Value,
        //            };
        //            stockDTO.SpeciesStocks.Add(speciesStockDTO);
        //        }

        //        return Ok(stockDTO);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        // GET: api/[id]/Stock
        // TODO: logic needs to be in a service
        // TODO: single store id function is not nessesary anymore?
        //[HttpGet("Stock")]
        //public async Task<IActionResult> GetStoresStock([FromQuery] int[] ids)
        //{
        //    try
        //    {
        //        if (ids.IsNullOrEmpty())
        //        {
        //            return NotFound("Stores not found."); //code 404
        //        }

        //        List<String> storeNames = [];
        //        foreach (var id in ids)
        //        {
        //            var store = await CheckIfStoreExist(id);
        //            storeNames.Add(store.Name);
        //        }

        //        var storesStock = await _storeRepository.GetStoresStock(ids);
        //        if (storesStock.IsNullOrEmpty())
        //        {
        //            return NotFound("Stores stock not found."); //code 404
        //        }

        //        // Initialize return DTO
        //        StockDTO stockDTO = new StockDTO
        //        {
        //            StoreName = String.Join(", ", [.. storeNames]),
        //            SpeciesStocks = []

        //        };

        //        // Add foreach species a speciesstockDTO to the stockDTO
        //        foreach (var stock in storesStock)
        //        {
        //            SpeciesStockDTO speciesStockDTO = new SpeciesStockDTO
        //            {
        //                SpeciesName = stock.Key,
        //                AnimalsAmount = stock.Value,
        //            };
        //            stockDTO.SpeciesStocks.Add(speciesStockDTO);
        //        }

        //        return Ok(stockDTO);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //// GET: api/Stores/{id}/Animals
        //[HttpGet("{id}/Animals")]
        //public async Task<IActionResult> GetStoreAnimals(int id)
        //{
        //    try
        //    {
        //        await CheckIfStoreExist(id);

        //        // Not via store.include() because we will need store info and animals seperatly.
        //        var animals = await _animalRepository.GetAnimalsByStoreId(id);

        //        if (animals.IsNullOrEmpty())
        //        {
        //            return NotFound("Animals not found."); //code 404
        //        }

        //        var animalsDTOs = animals.Select(a => new AnimalGetDTO(a)).ToList();

        //        return Ok(animalsDTOs);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //// GET: api/Stores/{id}/Species
        //[HttpGet("{id}/Species")]
        //public async Task<IActionResult> GetStoreSpecies(int id)
        //{
        //    try
        //    {
        //        await CheckIfStoreExist(id);

        //        var species = await _speciesRepository.GetSpeciesByStoreId(id);

        //        if (species.IsNullOrEmpty())
        //        {
        //            return NotFound("Species not found."); //code 404
        //        }

        //        var speciesDTOs = species.Select(s => new SpeciesGetDTO(s)).ToList();

        //        return Ok(speciesDTOs);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //// GET: api/Stores/{id}/Species/{speciesId}/Animals
        //[HttpGet("{id}/Species/{speciesId}/Animals")]
        //public async Task<IActionResult> GetStoreAnimalsBySpecies(int id, int speciesId)
        //{
        //    try
        //    {
        //        await CheckIfStoreExist(id);

        //        await CheckIfSpeciesExist(id);

        //        // TODO: Check if store contains species

        //        var animals = await _animalRepository.GetAnimalsByStoreIdAndSpieciesId(id, speciesId);

        //        if (animals.IsNullOrEmpty())
        //        {
        //            return NotFound("Animals not found."); //code 404
        //        }

        //        var animalsDTOs = animals.Select(a => new AnimalGetDTO(a)).ToList();

        //        return Ok(animalsDTOs);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

    }
}
