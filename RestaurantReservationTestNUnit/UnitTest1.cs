using Microsoft.EntityFrameworkCore;
using Moq;
using RestaurantReservation;
using RestaurantReservation.DbTables;
using RestaurantReservation.Repository.ServiceImplementation;
//using RestaurantReservation.Repository.ServiceImplementation;

namespace RestaurantReservationTestNUnit
{
    public class Tests
    {
        private Mock<RestaurantDbContext> _mockDbContext;
        private Mock<DbSet<CustomerTbl>> _mockDbSet;
        private CustomerManagement _customerManagement;
       // private Mock<RestaurantDbContext> _dbContext;
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseInMemoryDatabase(databaseName: "RestaurantReservation")
                .Options;

            // Initialize DbContext
            RestaurantDbContext _dbContext = new RestaurantDbContext(options);

            //  _mock = new Mock<CustomerManagement>();
            _mockDbSet = new Mock<DbSet<CustomerTbl>>();

            // Mock the DbContext
            _mockDbContext = new Mock<RestaurantDbContext>();
            //_mockDbContext.Setup(c =>c.Customers).Returns(_mockDbSet.Object);

            // Initialize CustomerManagement with the mocked DbContext
            _customerManagement = new CustomerManagement(_mockDbContext.Object);
        }

        [Test]
        public async Task GetAllCustomers_ShouldReturnCustomers()
        {
            // Act
            var customers = await _customerManagement.GetAllCustomers();

            // Assert
            Assert.IsNotNull(customers);
        }
    }
}