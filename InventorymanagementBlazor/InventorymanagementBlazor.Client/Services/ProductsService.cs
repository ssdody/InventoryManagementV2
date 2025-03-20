using InventoryManagementShared.Models;
using System.Net.Http.Json;


namespace InventorymanagementBlazor.Client.Services
{
 public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>("api/products");
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/products/{id}");
        }

        public async Task<bool> CreateProduct(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("api/products", product);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/products/{product.Id}", product);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/products/{id}");
            return response.IsSuccessStatusCode;
        }
    }

}
