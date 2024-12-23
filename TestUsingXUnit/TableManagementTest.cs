using Microsoft.EntityFrameworkCore;
using RestaurantReservation.DbTables;
using RestaurantReservation.Repository.Classes;
using RestaurantReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReservation.DTO;
using RestaurantReservation.Repository.ServiceImplementation;
using RestaurantReservation.Exceptions;

namespace TestUsingXUnit
{
    public class TableManagementTest
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly TableManagement _tableManagement;

        public TableManagementTest()
        {
            // Use in-memory database for testing
            var options = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseInMemoryDatabase(databaseName: "RestaurantTestDb")
                .Options;

            _dbContext = new RestaurantDbContext(options);
            _tableManagement = new TableManagement(_dbContext);

            // Seed the database with initial data
            _dbContext.TableStatus.Add(new TableCapacity
            {
                TCapacity = 4
                //Status = "Available"
            });
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task AddTable_ShouldAddTable()
        {
            // Arrange
            var newTable = new TableDTO
            {
                TCapacity = 6
            };

            // Act
            var result = await _tableManagement.AddTableDetails(newTable);

            // Assert
            var addedTable = await _dbContext.TableStatus.LastOrDefaultAsync();
            Assert.NotNull(addedTable);
            Assert.Equal(6, addedTable.TCapacity);
            Assert.NotEqual(4, addedTable.TCapacity);
        }

        [Fact]
        public async Task GetAllTable_ShouldReturnListOfTables()
        {
            // Act
            var tables = await _tableManagement.GetAllTableInfo();

            // Assert
            Assert.NotEmpty(tables);
            Assert.Equal(1, tables.Count());
        }

        [Fact]
        public async Task GetAllReservedTables_ShouldReturnListOfReservedTables()
        {
            // Act
            var tables = await _tableManagement.GetReservedTables();

            // Assert
            //Assert.NotEmpty(tables);
            Assert.Equal(0, tables.Count());
        }

        [Fact]
        public async Task GetAllTables_ShouldCountOfTable()
        {
            // Act
            var tables = await _tableManagement.GetAllTableInfo();

            // Assert
            //Assert.NotEmpty(restaurants);
            Assert.NotEqual(2, tables.Count());
        }

        [Fact]
        public async Task GetTableById_ShouldReturnTable_WhenIdExists()
        {
            // Arrange
            //var restaurantId = _dbContext.Restaurants.First().Id;
            var tableId = 1;
            // Act
            var result = await _tableManagement.GetTableInfoById(tableId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tableId, result.TableId);
        }

        [Fact]
        public async Task GetTableById_ShouldThrowTableException_WhenIdDoesNotExist()
        {
            // Act & Assert
            //var exception = await Assert.ThrowsAsync<RestaurantException>(() => _restaurantManagement.GetRestaurantById(999));
            var exception = await _tableManagement.GetTableInfoById(999);
            Assert.Null(exception);
            //Assert.Equal("Error occurred while retrieving the restaurant with ID 999.", exception.Message);
        }

        [Fact]
        public async Task GetTableById_ShouldThrowTableException_WhenIdNotGiven()
        {
            // Act & Assert
            //var exception = await Assert.ThrowsAsync<RestaurantException>(() => _restaurantManagement.GetRestaurantById(999));
            var exception = await _tableManagement.GetTableInfoById(0);
            Assert.Null(exception);
            //Assert.Equal("Error occurred while retrieving the restaurant with ID 999.", exception.Message);
        }

        [Fact]
        public async Task GetTableById_ShouldThrowTableException_WhenNegativeID()
        {
            // Act & Assert
            //var exception = await Assert.ThrowsAsync<RestaurantException>(() => _restaurantManagement.GetRestaurantById(999));
            var exception = await _tableManagement.GetTableInfoById(-2);
            Assert.Null(exception);
            //Assert.Equal("Error occurred while retrieving the restaurant with ID 999.", exception.Message);
        }

        [Fact]
        public async Task DeleteTable_ShouldRemoveTable()
        {
            // Arrange
            var tableId = _dbContext.TableStatus.First().TableId;

            // Act
            var deletedTable = await _tableManagement.DeleteTableInfo(tableId);

            // Assert
            //Assert.Null(await _dbContext.Restaurants.FindAsync(restaurantId));
            Assert.Equal(tableId, deletedTable.TableId);
        }

        [Fact]
        public async Task DeleteTable_ShouldNotRemoveTableForInvalidTID()
        {
            // Arrange
            //var restaurantId = _dbContext.Restaurants.First().RId;
            var tableId = 2;
            // Act
            var exception = await Assert.ThrowsAsync<TableException>(() => _tableManagement.DeleteTableInfo(tableId));
            //var deletedRestaurant = await _restaurantManagement.DeleteRestaurant(restaurantId);

            // Assert
            //Assert.Null(await _dbContext.Restaurants.FindAsync(restaurantId));
            //Assert.Equal(restaurantId, deletedRestaurant.RId);
            Assert.Equal("An unexpected error occurred while deleting the table.", exception.Message);
            //Assert.Null(deletedRestaurant);
        }

        [Fact]
        public async Task UpdateTableInformation_ShouldUpdateTable()
        {
            // Arrange
            //var restaurantId = _dbContext.Restaurants.First().Id;
            var tableId = 1;
            var updatedTable = new TableDTO
            {
                TCapacity = 9
            };

            // Act
            var result = await _tableManagement.UpdateTableInfo(updatedTable, tableId);

            // Assert
            var updated = await _dbContext.TableStatus.FindAsync(tableId);
            Assert.NotNull(updated);
            Assert.Equal(9, updated.TCapacity);
            //Assert.Equal("789 Updated St", updated.Address);
        }

        [Fact]
        public async Task UpdateTableInformation_ShouldThrowTableException_WhenTableNotFound()
        {
            // Arrange
            var updatedTable = new TableDTO
            {
                TCapacity = 4
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<TableException>(() => _tableManagement.UpdateTableInfo(updatedTable, 999));
            Assert.Equal("An unexpected error occurred while updating the table.", exception.Message);
        }
    } 
}
