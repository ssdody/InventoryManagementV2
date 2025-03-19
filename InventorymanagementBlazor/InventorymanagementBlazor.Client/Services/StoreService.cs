using InventoryManagementShared.Models;
using System.Net.Http.Json;

namespace InventorymanagementBlazor.Client.Services
{
    public class StoreService
    {
        private readonly HttpClient _httpClient;
        public StoreService(HttpClient httpClient) => _httpClient = httpClient;
        public async Task<List<Store>> GetStoresAsync() =>
            await _httpClient.GetFromJsonAsync<List<Store>>("api/stores");
    }
}
