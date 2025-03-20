using InventoryManagement.Data;
using InventoryManagementShared.Models;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Repositories;

namespace InventoryManagement.Repositories
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        private readonly ApplicationDbContext _context;

        public StoreRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Existing method to get stores
        public async Task<IEnumerable<Store>> GetAllStoresAsync()
        {
            return await _context.Stores.ToListAsync();
        }

        public async Task<Store> GetStoreByIdAsync(int id)
        {
            return await _context.Stores.FindAsync(id);
        }

        public async Task<Store> AddStoreAsync(Store store)
        {
            await _context.Stores.AddAsync(store);
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task<Store> UpdateStoreAsync(Store store)
        {
            _context.Stores.Update(store);
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task DeleteStoreAsync(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                _context.Stores.Remove(store);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Store>> GetStoresWithProductsAsync()
        {
            return await _context.Stores
                .Include(store => store.Products) 
                .ToListAsync();
        }
    }
}
