using InventoryManagement.Data;
using InventoryManagement.Repositories;
using InventoryManagementShared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace InventoryManagement.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        private ApplicationDbContext GetDbContext()
        {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
        }   

        [Fact]
        public async Task GetProductsByStoreIdAsync_ShouldReturnProductsForStore()
        {
            // Arrange
            using var context = GetDbContext();
            await context.Products.AddRangeAsync(
                new Product { Id = 1, Name = "Product1", StoreId = 1 },
                new Product { Id = 2, Name = "Product2", StoreId = 1 },
                new Product { Id = 3, Name = "Product3", StoreId = 2 }
            );
            await context.SaveChangesAsync();

            var repository = new ProductRepository(context);

            // Act
            var result = await repository.GetProductsByStoreIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, p => Assert.Equal(1, p.StoreId));
        }

        // Test method to get a product by Id
        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnCorrectProduct()
        {
            // Arrange
            var context = GetDbContext();
            var product = new Product { Id = 1, Name = "Product1", StoreId = 1 };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var repository = new ProductRepository(context);

            // Act
            var result = await repository.GetProductByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Product1", result.Name);
        }

        // Test method to add a new product
        [Fact]
        public async Task AddProductAsync_ShouldAddProductToDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var repository = new ProductRepository(context);
            var newProduct = new Product { Id = 2, Name = "New Product", StoreId = 1 };

            // Act
            var addedProduct = await repository.AddProductAsync(newProduct);
            var result = await context.Products.FindAsync(2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Product", result.Name);
            Assert.Equal(1, result.StoreId);
        }

        // Test method to update an existing product
        [Fact]
        public async Task UpdateProductAsync_ShouldUpdateProduct()
        {
            // Arrange
            var context = GetDbContext();
            var product = new Product { Id = 1, Name = "Old Product", StoreId = 1 };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var repository = new ProductRepository(context);
            product.Name = "Updated Product";

            // Act
            var updatedProduct = await repository.UpdateProductAsync(product);
            var result = await context.Products.FindAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Product", result.Name);
        }

        // Test method to delete a product
        [Fact]
        public async Task DeleteProductAsync_ShouldRemoveProduct()
        {
            // Arrange
            var context = GetDbContext();
            var product = new Product { Id = 1, Name = "Product To Delete", StoreId = 1 };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var repository = new ProductRepository(context);

            // Act
            await repository.DeleteProductAsync(1);
            var result = await context.Products.FindAsync(1);

            // Assert
            Assert.Null(result); // Product should be removed
        }
    }
}
