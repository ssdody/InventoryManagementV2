using InventoryManagementShared.Models;
using InventoryManagement.Repositories;

namespace InventoryManagement.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<IEnumerable<Store>> GetAllStoresAsync()
        {
            return await _storeRepository.GetAllAsync();
        }

        public async Task<Store?> GetStoreByIdAsync(int id)
        {
            return await _storeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Store>> GetStoresWithProductsAsync()
        {
            return await _storeRepository.GetStoresWithProductsAsync();
        }

        public async Task AddStoreAsync(Store store)
        {
            await _storeRepository.AddAsync(store);
            await _storeRepository.SaveChangesAsync();
        }

        public async Task UpdateStoreAsync(Store store)
        {
            _storeRepository.Update(store);
            await _storeRepository.SaveChangesAsync();
        }

        public async Task DeleteStoreAsync(int id)
        {
            var store = await _storeRepository.GetByIdAsync(id);
            if (store != null)
            {
                _storeRepository.Delete(store);
                await _storeRepository.SaveChangesAsync();
            }
        }
    }
}
