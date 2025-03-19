using InventoryManagementShared.Models;

namespace InventoryManagement.Repositories
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<IEnumerable<Store>> GetStoresWithProductsAsync();
    }
}
