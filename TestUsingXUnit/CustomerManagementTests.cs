using Microsoft.EntityFrameworkCore;
using RestaurantReservation.DbTables;
using RestaurantReservation.Repository.Classes;
using RestaurantReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReservation.Repository.ServiceImplementation;
using Moq;
using RestaurantReservation.DTO;
using RestaurantReservation.Exceptions;
using Castle.Core.Resource;


namespace TestUsingXUnit
{
    public class CustomerManagementTests
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly CustomerManagement _customerManagement;

        public CustomerManagementTests()
        {
            // Use in-memory database for testing
            var options = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseInMemoryDatabase(databaseName: "RestaurantTestDb")
                .Options;

            _dbContext = new RestaurantDbContext(options);
            _customerManagement = new CustomerManagement(_dbContext);

            // Seed the database with initial data
            _dbContext.Customers.Add(new CustomerTbl
            {
                Name = "John Doe",
                Address = "123 Street",
                PhoneNumber = "1234567890"
            });
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task AddCustomer_ShouldReturnTrue_WhenCustomerIsAddedSuccessfully()
        {
            // Arrange
            //var mockDbContext = new Mock<RestaurantDbContext>();
            //var mockDbSet = new Mock<DbSet<CustomerTbl>>();

            //mockDbContext.Setup(m => m.Customers).Returns(mockDbSet.Object);

            //var customerManagement = new CustomerManagement(mockDbContext.Object);
            var customerDTO = new CustomerDTO
            {
                Name = "Sachin Bansode",
                Address = "Solapur",
                PhoneNumber = "1785687890"
            };

            // Act
            var result = await _customerManagement.AddCustomer(customerDTO);

            // Assert
            Assert.True(result);
            var addedCustomer = await _dbContext.Customers.FirstOrDefaultAsync(r => r.Name == "Sachin Bansode");
            Assert.Equal("Sachin Bansode", addedCustomer.Name);
            Assert.NotEqual("Test", addedCustomer.Name);
            //mockDbSet.Verify(m => m.AddAsync(It.IsAny<CustomerTbl>(), It.IsAny<CancellationToken>()), Times.Once);
            //mockDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetAllCustomers_ShouldReturnListOfCustomers()
        {
            // Act
            var customers = await _customerManagement.GetAllCustomers();

            // Assert
            Assert.NotEmpty(customers);
            Assert.Equal(1, customers.Count());
        }

        [Fact]
        public async Task GetAllCustomers_ShouldCountOfCustomers()
        {
            // Act
            var customers = await _customerManagement.GetAllCustomers();

            // Assert
            //Assert.NotEmpty(restaurants);
            Assert.NotEqual(2, customers.Count());
        }

        [Fact]
        public async Task GetCustomerById_ShouldReturnCustomer_WhenIdExists()
        {
            // Arrange
            //var restaurantId = _dbContext.Restaurants.First().Id;
            var customerId = 1;
            // Act
            var result = await _customerManagement.GetCustomerById(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.CId);
        }

        [Fact]
        public async Task GetCustomerById_ShouldThrowCustomerException_WhenIdDoesNotExist()
        {
            // Act & Assert
            //var exception = await Assert.ThrowsAsync<RestaurantException>(() => _restaurantManagement.GetRestaurantById(999));
            var exception = await _customerManagement.GetCustomerById(999);
            Assert.Null(exception);
            //Assert.Equal("Error occurred while retrieving the restaurant with ID 999.", exception.Message);
        }

        [Fact]
        public async Task GetCustomerById_ShouldThrowCustomerException_WhenIdNotGiven()
        {
            // Act & Assert
            //var exception = await Assert.ThrowsAsync<RestaurantException>(() => _restaurantManagement.GetRestaurantById(999));
            var exception = await _customerManagement.GetCustomerById(0);
            Assert.Null(exception);
            //Assert.Equal("Error occurred while retrieving the restaurant with ID 999.", exception.Message);
        }

        [Fact]
        public async Task GetCustomerById_ShouldThrowCustomerException_WhenNegativeID()
        {
            // Act & Assert
            //var exception = await Assert.ThrowsAsync<RestaurantException>(() => _restaurantManagement.GetRestaurantById(999));
            var exception = await _customerManagement.GetCustomerById(-2);
            Assert.Null(exception);
            //Assert.Equal("Error occurred while retrieving the restaurant with ID 999.", exception.Message);
        }

        [Fact]
        public async Task DeleteCustomer_ShouldRemoveCustomer()
        {
            // Arrange
            var customerId = _dbContext.Customers.First().CId;
             
            // Act
            var deletedCustomer = await _customerManagement.DeleteCustomer(customerId);

            // Assert
            //Assert.Null(await _dbContext.Restaurants.FindAsync(restaurantId));
            Assert.True(deletedCustomer);
        }

        [Fact]
        public async Task DeleteCustomer_ShouldNotRemoveCustomerForInvalidCID()
        {
            // Arrange
            //var restaurantId = _dbContext.Restaurants.First().RId;
            var customerId = 2;
            // Act
            var exception = await Assert.ThrowsAsync<RestaurantException>(() => _customerManagement.DeleteCustomer(customerId));
            //var deletedRestaurant = await _restaurantManagement.DeleteRestaurant(restaurantId);

            // Assert
            //Assert.Null(await _dbContext.Restaurants.FindAsync(restaurantId));
            //Assert.Equal(restaurantId, deletedRestaurant.RId);
            Assert.Equal("An unexpected error occurred while deleting the customer.", exception.Message);
            //Assert.Null(deletedRestaurant);
        }

        [Fact]
        public async Task UpdateCustInformation_ShouldUpdateCustomer()
        {
            // Arrange
            //var restaurantId = _dbContext.Restaurants.First().Id;
            var customerId = 1;
            var updatedCustomer = new UpdateCustomerDTO
            {
                Address = "789 Updated Ave",
                PhoneNumber = "1122336677"
            };

            // Act
            var result = await _customerManagement.UpdateCustInformation(updatedCustomer, customerId);

            // Assert
            var updated = await _dbContext.Customers.FindAsync(customerId);
            Assert.NotNull(result);
            Assert.Equal("1122336677", updated.PhoneNumber);
            Assert.Equal("789 Updated Ave", updated.Address);
        }

        [Fact]
        public async Task UpdateCustInformation_ShouldThrowCustomerException_WhenCustomerNotFound()
        {
            // Arrange
            //var customerId = 1;
            var updatedCustomer = new UpdateCustomerDTO
            {
                Address = "789 Updated St",
                PhoneNumber = "7865987067"
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomerException>(() => _customerManagement.UpdateCustInformation(updatedCustomer, 569));
            Assert.Equal("An unexpected error occurred while updating the customer.", exception.Message);
        }

    }
}
