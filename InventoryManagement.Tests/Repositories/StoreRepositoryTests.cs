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
    public class StoreRepositoryTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            var context = new ApplicationDbContext(options);

            // Clear the database before each test
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        // Test for GetAllStoresAsync to fetch all stores
        [Fact]
        public async Task GetAllStoresAsync_ShouldReturnAllStores()
        {
            // Arrange
            var context = GetDbContext();
            context.Stores.AddRange(
                new Store { Id = 1, Name = "Store1", Description = "Store1 Description" },
                new Store { Id = 2, Name = "Store2", Description = "Store2 Description" }
            );
            await context.SaveChangesAsync();

            var repository = new StoreRepository(context);

            // Act
            var result = await repository.GetAllStoresAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetStoreByIdAsync_ShouldReturnCorrectStore()
        {
            // Arrange
            var context = GetDbContext();
            var store = new Store { Name = "Store1", Description = "Store1 Description" };
            context.Stores.Add(store);
            await context.SaveChangesAsync();  // Let EF handle the Id assignment

            var repository = new StoreRepository(context);

            // Act
            var result = await repository.GetStoreByIdAsync(store.Id);  // Use the assigned Id

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Store1", result.Name);
        }


        // Test for AddStoreAsync to add a new store
        [Fact]
        public async Task AddStoreAsync_ShouldAddStoreToDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var repository = new StoreRepository(context);
            var newStore = new Store { Id = 3, Name = "New Store", Description = "New Store Description" };

            // Act
            var addedStore = await repository.AddStoreAsync(newStore);
            var result = await context.Stores.FindAsync(3);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Store", result.Name);
        }

        // Test for UpdateStoreAsync to update an existing store
        [Fact]
        public async Task UpdateStoreAsync_ShouldUpdateStore()
        {
            // Arrange
            var context = GetDbContext();
            var store = new Store { Id = 1, Name = "Old Store", Description = "Old Store Description" };
            context.Stores.Add(store);
            await context.SaveChangesAsync();

            var repository = new StoreRepository(context);
            store.Name = "Updated Store";

            // Act
            var updatedStore = await repository.UpdateStoreAsync(store);
            var result = await context.Stores.FindAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Store", result.Name);
        }

        // Test for DeleteStoreAsync to delete a store by its ID
        [Fact]
        public async Task DeleteStoreAsync_ShouldRemoveStore()
        {
            // Arrange
            var context = GetDbContext();
            var store = new Store { Id = 1, Name = "Store To Delete", Description = "Store To Delete Description" };
            context.Stores.Add(store);
            await context.SaveChangesAsync();

            var repository = new StoreRepository(context);

            // Act
            await repository.DeleteStoreAsync(1);
            var result = await context.Stores.FindAsync(1);

            // Assert
            Assert.Null(result); // Store should be removed
        }

        // Test for GetStoresWithProductsAsync to fetch stores along with their products
        [Fact]
        public async Task GetStoresWithProductsAsync_ShouldReturnStoresWithProducts()
        {
            // Arrange
            var context = GetDbContext();
            var store1 = new Store { Id = 1, Name = "Store1", Description = "Store1 Description", Products = new List<Product> { new Product { Id = 1, Name = "Product1", StoreId = 1 } } };
            var store2 = new Store { Id = 2, Name = "Store2", Description = "Store2 Description", Products = new List<Product> { new Product { Id = 2, Name = "Product2", StoreId = 2 } } };

            context.Stores.Add(store1);
            context.Stores.Add(store2);
            await context.SaveChangesAsync();

            var repository = new StoreRepository(context);

            // Act
            var result = await repository.GetStoresWithProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count()); // We have two stores
            Assert.All(result, store => Assert.NotEmpty(store.Products)); // Each store has at least one product
        }
    }
}
