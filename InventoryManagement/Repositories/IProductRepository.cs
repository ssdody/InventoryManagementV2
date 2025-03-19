using InventoryManagementShared.Models;

namespace InventoryManagement.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByStoreIdAsync(int storeId);
    }
}
