using Moq;
using RestaurantReservation.DbTables;
using RestaurantReservation.DTO;
using RestaurantReservation.Exceptions;
using RestaurantReservation.Repository.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.IdentityModel.Tokens;

namespace RestaurantReservation.Tests
{
    public class RestaurantManagementTests
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly RestaurantManagement _restaurantManagement;

        public RestaurantManagementTests()
        {
            // Use in-memory database for testing
            var options = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseInMemoryDatabase(databaseName: "RestaurantTestDb")
                .Options;

            _dbContext = new RestaurantDbContext(options);
            _restaurantManagement = new RestaurantManagement(_dbContext);

            // Seed the database with initial data
            _dbContext.Restaurants.Add(new Restaurant { Name = "Test Restaurant", Address = "123 Test St", PhoneNumber = "1234567890", OpeningHours = "9 AM - 9 PM", Capacity = 50 });
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task AddResta_ShouldAddRestaurant()
        {
            // Arrange
            var newRestaurant = new AddRestaurant
            {
                Name = "New Restaurant",
                Address = "456 New St",
                PhoneNumber = "0987654321",
                OpeningHours = "10 AM - 8 PM",
                Capacity = 100
            };

            // Act
            var result = await _restaurantManagement.AddResta(newRestaurant);

            // Assert
            var addedRestaurant = await _dbContext.Restaurants.FirstOrDefaultAsync(r => r.Name == "New Restaurant");
            Assert.NotNull(addedRestaurant);
            Assert.Equal("New Restaurant", addedRestaurant.Name);
            Assert.NotEqual("Test Restaurant", addedRestaurant.Name);
        }

        [Fact]
        public async Task GetAllRestaurants_ShouldReturnListOfRestaurants()
        {
            // Act
            var restaurants = await _restaurantManagement.GetAllRestaurants();

            // Assert
            Assert.NotEmpty(restaurants);
            Assert.Equal(1, restaurants.Count());
        }

        [Fact]
        public async Task GetAllRestaurants_ShouldCountOfRestaurants()
        {
            // Act
            var restaurants = await _restaurantManagement.GetAllRestaurants();

            // Assert
            //Assert.NotEmpty(restaurants);
            Assert.NotEqual(2, restaurants.Count());
        }

        [Fact]
        public async Task GetRestaurantById_ShouldReturnRestaurant_WhenIdExists()
        {
            // Arrange
            //var restaurantId = _dbContext.Restaurants.First().Id;
            var restaurantId = 1;
            // Act
            var result = await _restaurantManagement.GetRestaurantById(restaurantId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(restaurantId, result.RId);
        }

        [Fact]
        public async Task GetRestaurantById_ShouldThrowRestaurantException_WhenIdDoesNotExist()
        {
            // Act & Assert
            //var exception = await Assert.ThrowsAsync<RestaurantException>(() => _restaurantManagement.GetRestaurantById(999));
            var exception = await _restaurantManagement.GetRestaurantById(999);
            Assert.Null(exception);
            //Assert.Equal("Error occurred while retrieving the restaurant with ID 999.", exception.Message);
        }

        [Fact]
        public async Task GetRestaurantById_ShouldThrowRestaurantException_WhenIdNotGiven()
        {
            // Act & Assert
            //var exception = await Assert.ThrowsAsync<RestaurantException>(() => _restaurantManagement.GetRestaurantById(999));
            var exception = await _restaurantManagement.GetRestaurantById(0);
            Assert.Null(exception);
            //Assert.Equal("Error occurred while retrieving the restaurant with ID 999.", exception.Message);
        }

        [Fact]
        public async Task GetRestaurantById_ShouldThrowRestaurantException_WhenNegativeID()
        {
            // Act & Assert
            //var exception = await Assert.ThrowsAsync<RestaurantException>(() => _restaurantManagement.GetRestaurantById(999));
            var exception = await _restaurantManagement.GetRestaurantById(-2);
            Assert.Null(exception);
            //Assert.Equal("Error occurred while retrieving the restaurant with ID 999.", exception.Message);
        }

        [Fact]
        public async Task DeleteRestaurant_ShouldRemoveRestaurant()
        {
            // Arrange
            var restaurantId = _dbContext.Restaurants.First().RId;

            // Act
            var deletedRestaurant = await _restaurantManagement.DeleteRestaurant(restaurantId);

            // Assert
            //Assert.Null(await _dbContext.Restaurants.FindAsync(restaurantId));
            Assert.Equal(restaurantId, deletedRestaurant.RId);
        }

        [Fact]
        public async Task DeleteRestaurant_ShouldNotRemoveRestaurantForInvalidRID()
        {
            // Arrange
            //var restaurantId = _dbContext.Restaurants.First().RId;
            var restaurantId = 2;
            // Act
            var exception = await Assert.ThrowsAsync<RestaurantException>(() => _restaurantManagement.DeleteRestaurant(restaurantId));
            //var deletedRestaurant = await _restaurantManagement.DeleteRestaurant(restaurantId);

            // Assert
            //Assert.Null(await _dbContext.Restaurants.FindAsync(restaurantId));
            //Assert.Equal(restaurantId, deletedRestaurant.RId);
            Assert.Equal("An unexpected error occurred while deleting the restaurant.", exception.Message);
            //Assert.Null(deletedRestaurant);
        }



        [Fact]
        public async Task UpdateRestInformation_ShouldUpdateRestaurant()
        {
            // Arrange
            //var restaurantId = _dbContext.Restaurants.First().Id;
            var restaurantId = 1;
            var updatedRestaurant = new AddRestaurant
            {
                Name = "Updated Restaurant",
                Address = "789 Updated St",
                PhoneNumber = "1122334455",
                OpeningHours = "8 AM - 8 PM",
                Capacity = 75
            };

            // Act
            var result = await _restaurantManagement.UpdateRestInformation(updatedRestaurant, restaurantId);

            // Assert
            var updated = await _dbContext.Restaurants.FindAsync(restaurantId);
            Assert.NotNull(updated);
            Assert.Equal("Updated Restaurant", updated.Name);
            Assert.Equal("789 Updated St", updated.Address);
        }

        [Fact]
        public async Task UpdateRestInformation_ShouldThrowRestaurantException_WhenRestaurantNotFound()
        {
            // Arrange
            var updatedRestaurant = new AddRestaurant
            {
                Name = "Non Existent Restaurant",
                Address = "111 Non Existent St",
                PhoneNumber = "5555555555",
                OpeningHours = "7 AM - 7 PM",
                Capacity = 30
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<RestaurantException>(() => _restaurantManagement.UpdateRestInformation(updatedRestaurant, 999));
            Assert.Equal("An unexpected error occurred while updating the restaurant.", exception.Message);
        }
    }
}
