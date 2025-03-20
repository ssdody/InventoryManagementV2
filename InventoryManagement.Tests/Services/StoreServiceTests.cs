using InventoryManagement.Repositories;
using InventoryManagement.Services;
using InventoryManagementShared.Models;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace InventoryManagement.Tests.Services
{
    public class StoreServiceTests
    {
        private readonly Mock<IStoreRepository> _mockStoreRepository;
        private readonly StoreService _storeService;

        public StoreServiceTests()
        {
            // Setup mock for IStoreRepository
            _mockStoreRepository = new Mock<IStoreRepository>();
            _storeService = new StoreService(_mockStoreRepository.Object);
        }

        [Fact]
        public async Task GetAllStoresAsync_ShouldReturnStores()
        {
            // Arrange
            var stores = new List<Store>
            {
                new Store { Id = 1, Name = "Store 1" },
                new Store { Id = 2, Name = "Store 2" }
            };

            // Mock the GetAllAsync method
            _mockStoreRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(stores);

            // Act
            var result = await _storeService.GetAllStoresAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, s => s.Name == "Store 1");
            Assert.Contains(result, s => s.Name == "Store 2");
        }


        [Fact]
        public async Task GetStoreByIdAsync_ShouldReturnStore()
        {
            // Arrange
            var store = new Store { Id = 1, Name = "Store 1" };

            // Mock GetByIdAsync method
            _mockStoreRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(store);

            // Act
            var result = await _storeService.GetStoreByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Store 1", result.Name);
        }

        [Fact]
        public async Task GetStoreByIdAsync_ShouldReturnNull_WhenStoreNotFound()
        {
            // Arrange
            // Mock GetByIdAsync method to return null
            _mockStoreRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Store)null);

            // Act
            var result = await _storeService.GetStoreByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddStoreAsync_ShouldCallAddAndSave()
        {
            // Arrange
            var store = new Store { Id = 1, Name = "Store 1" };

            // Act
            await _storeService.AddStoreAsync(store);

            // Assert
            _mockStoreRepository.Verify(repo => repo.AddAsync(It.IsAny<Store>()), Times.Once);
            _mockStoreRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateStoreAsync_ShouldCallUpdateAndSave()
        {
            // Arrange
            var store = new Store { Id = 1, Name = "Store 1" };

            // Act
            await _storeService.UpdateStoreAsync(store);

            // Assert
            _mockStoreRepository.Verify(repo => repo.Update(It.IsAny<Store>()), Times.Once);
            _mockStoreRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteStoreAsync_ShouldCallDeleteAndSave()
        {
            // Arrange
            var store = new Store { Id = 1, Name = "Store 1" };

            // Mock GetByIdAsync to return the store for deletion
            _mockStoreRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(store);

            // Act
            await _storeService.DeleteStoreAsync(1);

            // Assert
            _mockStoreRepository.Verify(repo => repo.Delete(It.IsAny<Store>()), Times.Once);
            _mockStoreRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteStoreAsync_ShouldNotCallDelete_WhenStoreNotFound()
        {
            // Arrange
            // Mock GetByIdAsync to return null
            _mockStoreRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Store)null);

            // Act
            await _storeService.DeleteStoreAsync(1);

            // Assert
            _mockStoreRepository.Verify(repo => repo.Delete(It.IsAny<Store>()), Times.Never);
            _mockStoreRepository.Verify(repo => repo.SaveChangesAsync(), Times.Never);
        }
    }
}
