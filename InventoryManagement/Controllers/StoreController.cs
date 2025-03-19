using InventoryManagementShared.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/stores")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        // GET: api/stores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStores()
        {
            var stores = await _storeService.GetAllStoresAsync();
            return Ok(stores);
        }

        // GET: api/stores/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore(int id)
        {
            var store = await _storeService.GetStoreByIdAsync(id);
            if (Object.Equals( store, null))
                return NotFound();
            else
            return Ok(store);
        }

        // GET: api/stores/with-products
        // [HttpGet("with-products")]
        // public async Task<ActionResult<IEnumerable<Store>>> GetStoresWithProducts()
        // {
        //     var stores = await _storeService.GetStoresWithProductsAsync();
        //     return Ok(stores);
        // }

        // POST: api/stores
        [HttpPost]
        public async Task<ActionResult<Store>> CreateStore(Store store)
        {
            await _storeService.AddStoreAsync(store);
            return CreatedAtAction(nameof(GetStore), new { id = store.Id }, store);
        }

        // PUT: api/stores/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(int id, Store store)
        {
            if (id != store.Id)
                return BadRequest();

            await _storeService.UpdateStoreAsync(store);
            return NoContent();
        }

        // DELETE: api/stores/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            await _storeService.DeleteStoreAsync(id);
            return NoContent();
        }
    }
}
