using InventoryManagementShared.Models;

namespace InventorymanagementBlazor.Client.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetProductsAsync();
        Task<Product?> GetProductById(int id);
        Task<bool> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
    }

}
