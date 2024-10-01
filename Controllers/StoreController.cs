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

        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
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
