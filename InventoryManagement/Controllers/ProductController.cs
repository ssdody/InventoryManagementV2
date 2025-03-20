using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementShared.Models;

namespace InventoryManagement.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product?>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (Object.Equals(product, null))
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
            
        }

        // GET: api/products/store/{storeId}
        [HttpGet("store/{storeId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByStoreId(int storeId)
        {
            var products = await _productService.GetProductsByStoreIdAsync(storeId);
            return Ok(products);
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpOptions]
        public IActionResult Options()
        {
            return Ok();
        }
    }
}
