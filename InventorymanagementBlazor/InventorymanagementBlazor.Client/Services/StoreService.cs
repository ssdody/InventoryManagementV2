using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using InventoryManagementShared.Models;

namespace InventorymanagementBlazor.Client.Services
{
    public class StoreService : IStoreService
    {
        private readonly HttpClient _httpClient;

        public StoreService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Store>> GetStoresAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Store>>("api/stores");
        }
    }
}
