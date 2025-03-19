using InventoryManagementShared.Models;

namespace InventoryManagement.Services
{
    public interface IStoreService
    {
        Task<IEnumerable<Store>> GetAllStoresAsync();
        Task<Store?> GetStoreByIdAsync(int id);
        // Task<IEnumerable<Store>> GetStoresWithProductsAsync();
        Task AddStoreAsync(Store store);
        Task UpdateStoreAsync(Store store);
        Task DeleteStoreAsync(int id);
    }
}
