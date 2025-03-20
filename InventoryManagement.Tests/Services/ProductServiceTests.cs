using InventoryManagement.Repositories;
using InventoryManagement.Services;
using InventoryManagementShared.Models;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace InventoryManagement.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            // Setup mock for IProductRepository
            _mockProductRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockProductRepository.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Quantity = 10, Price = 100 },
                new Product { Id = 2, Name = "Product 2", Quantity = 20, Price = 150 }
            };

            _mockProductRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.Name == "Product 1");
            Assert.Contains(result, p => p.Name == "Product 2");
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Quantity = 10, Price = 100 };
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(It.Is<int>(id => id == 1))).ReturnsAsync(product);

            // Act
            var result = await _productService.GetProductByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Product 1", result.Name);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnNull_WhenProductNotFound()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(It.Is<int>(id => id == 1))).ReturnsAsync((Product?)null);

            // Act
            var result = await _productService.GetProductByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddProductAsync_ShouldCallAddAndSave()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Quantity = 10, Price = 100 };

            // Act
            await _productService.AddProductAsync(product);

            // Assert
            _mockProductRepository.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
            _mockProductRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldCallUpdateAndSave()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Quantity = 10, Price = 100 };

            // Act
            await _productService.UpdateProductAsync(product);

            // Assert
            _mockProductRepository.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Once);
            _mockProductRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldCallDeleteAndSave()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Quantity = 10, Price = 100 };
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(It.Is<int>(id => id == 1))).ReturnsAsync(product);

            // Act
            await _productService.DeleteProductAsync(1);

            // Assert
            _mockProductRepository.Verify(repo => repo.Delete(It.IsAny<Product>()), Times.Once);
            _mockProductRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldNotCallDelete_WhenProductNotFound()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(It.Is<int>(id => id == 1))).ReturnsAsync((Product?)null);

            // Act
            await _productService.DeleteProductAsync(1);

            // Assert
            _mockProductRepository.Verify(repo => repo.Delete(It.IsAny<Product>()), Times.Never);
            _mockProductRepository.Verify(repo => repo.SaveChangesAsync(), Times.Never);
        }
    }
}
