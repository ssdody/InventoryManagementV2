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

        public async Task<Store?> GetStoreById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Store>($"api/stores/{id}");
        }


        public async Task<bool> CreateStore(Store store)
        {
            Console.WriteLine("CreateStore service",store);
            var response = await _httpClient.PostAsJsonAsync("api/stores", store);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateStore(Store store)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/stores/{store.Id}", store);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteStore(int storeId)
        {
            var response = await _httpClient.DeleteAsync($"api/stores/{storeId}");
            return response.IsSuccessStatusCode;
        }
    }
}
