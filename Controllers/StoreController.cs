using Microsoft.AspNetCore.Mvc;
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
        public Task<ActionResult<IEnumerable<Store>>> GetStores()
        {
            throw new NotImplementedException();
        }

        // GET: api/Stores/id
        [HttpGet("{id}")]
        public async Task<StoreDTO> GetStore(int id)
        {
            try
            {
                return new StoreDTO(await _storeRepository.GetStoreByID(id));
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
            throw new NotImplementedException();
        }

        // POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Store>> PostStore(Store store)
        {
            throw new NotImplementedException();
        }

            // DELETE: api/Stores/id
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
                throw new NotImplementedException();
        }
    }
}
