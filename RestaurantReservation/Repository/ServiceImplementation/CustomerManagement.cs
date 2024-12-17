using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RestaurantReservation.DbTables;
using RestaurantReservation.DTO;
using RestaurantReservation.Exceptions;
using RestaurantReservation.Repository.Services;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace RestaurantReservation.Repository.ServiceImplementation
{
    public class CustomerManagement : ICustomer
    {
        private readonly RestaurantDbContext _dbContext;
        public CustomerManagement(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddCustomer(CustomerDTO cust)
        {

            try
            {
                await _dbContext.Customers.AddAsync( new CustomerTbl
                {
                    Name = cust.Name,
                    Address = cust.Address,
                    PhoneNumber = cust.PhoneNumber
                });
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new CustomerException("Error occurred while adding the customer.", ex);
            }
            catch (Exception ex)
            {
                throw new RestaurantException("An unexpected error occurred while adding the customer.", ex);
            }
        }

        public async Task<IEnumerable<CustomerTbl>> GetAllCustomers()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<CustomerTbl?> GetCustomerById(int id)
        {
            try
            {
                CustomerTbl? cust = null;
                try
                {
                    cust = await _dbContext.Customers.FindAsync(id);
                }
                catch (Exception ex)
                {
                    throw new RestaurantException("An unexpected error occurred while retrieving the customer.", ex);
                }
                return cust;
            }
            catch (Exception ex)
            {
                throw new RestaurantException($"Error occurred while retrieving the customer with ID {id}.", ex);
            }
        }
        public async Task<bool> DeleteCustomer(int cid)
        {
            try
            {
                var excust = await _dbContext.Customers.FindAsync(cid);
                _dbContext.Customers.Remove(excust);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new RestaurantException("Error occurred while deleting the customer.", ex);
            }
            catch (Exception ex)
            {
                throw new RestaurantException("An unexpected error occurred while deleting the customer.", ex);
            }
        }

        public async Task<CustomerTbl> UpdateCustInformation(UpdateCustomerDTO cust, int i)
        {
            try
            {
                var excust = await _dbContext.Customers.FindAsync(i);
                excust.Address = cust.Address;
                excust.PhoneNumber = cust.PhoneNumber;
                _dbContext.Customers.Update(excust);
                await _dbContext.SaveChangesAsync();
                return excust;
            }
            catch (DbUpdateException ex)
            {
                throw new RestaurantException("Error occurred while updating the customer.", ex);
            }
            catch (Exception ex)
            {
                throw new RestaurantException("An unexpected error occurred while updating the customer.", ex);
            }
        }
    }
}
