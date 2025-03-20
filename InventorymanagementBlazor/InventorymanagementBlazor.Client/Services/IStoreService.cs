using InventoryManagementShared.Models;

namespace InventorymanagementBlazor.Client.Services
{
    public interface IStoreService
    {
        public Task<List<Store>> GetStoresAsync();
        Task<bool> CreateStore(Store store);
        Task<Store?> GetStoreById(int storeId);
        Task<bool> UpdateStore(Store store);
        Task<bool> DeleteStore(int storeId);
    }
}
