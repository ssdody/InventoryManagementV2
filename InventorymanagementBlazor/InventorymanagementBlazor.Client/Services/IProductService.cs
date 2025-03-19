using InventoryManagementShared.Models;

namespace InventorymanagementBlazor.Client.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetProductsAsync();
    }

}
