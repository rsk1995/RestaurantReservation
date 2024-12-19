using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantReservation.Repository.ServiceImplementation;
using RestaurantReservation.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Tests
{
    internal class CustomerManagementTests
    {
        private Mock<ICustomer> _cust;
        private Mock<CustomerManagement> cust;
        

        [SetUp]
        public void SetUp()
        {
            _cust = new Mock<ICustomer>();
           // cust = new CustomerManagement();
        }

        [Test]
        public async Task GetCustomersAsync()
        {
            var result = await _cust.Object.GetAllCustomers();

            //Assert.AreEqual(1,result.Count());
            //Assert.IsNotNull(result);
            Console.WriteLine(result.Count());
            Console.ReadLine();
        }

        [Test]
        public async Task GetCustomerById()
        {
            

        }

       
    }
}
