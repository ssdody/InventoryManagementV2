using InventoryManagementShared.Models;
using System.Net.Http.Json;

namespace InventorymanagementBlazor.Client.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient) => _httpClient = httpClient;
        public async Task<List<Product>> GetProductsAsync() =>
            await _httpClient.GetFromJsonAsync<List<Product>>("api/products");
    }

}
