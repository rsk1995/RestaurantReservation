using RestaurantReservation.DbTables;
using RestaurantReservation.DTO;

namespace RestaurantReservation.Repository.Services
{
    public interface ICustomer
    {
        Task<bool> AddCustomer(CustomerDTO cust);
        Task<IEnumerable<CustomerTbl>> GetAllCustomers();
        Task<CustomerTbl?> GetCustomerById(int id);
        Task<bool> DeleteCustomer(int cid);

        Task<CustomerTbl> UpdateCustInformation(UpdateCustomerDTO cust, int i);
    }
}
