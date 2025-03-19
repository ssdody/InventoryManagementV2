using InventoryManagementShared.Models;

namespace InventorymanagementBlazor.Client.Services
{
    public interface IStoreService
    {
        public Task<List<Store>> GetStoresAsync();
    }
}
