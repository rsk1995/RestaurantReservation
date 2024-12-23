using Microsoft.EntityFrameworkCore;
using RestaurantReservation.DbTables;
using RestaurantReservation.DTO;
using RestaurantReservation.Exceptions;
using RestaurantReservation.Repository.Interfaces;

namespace RestaurantReservation.Repository.Classes
{
    public class RestaurantManagement : IRestaurant
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantManagement(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;            
        }
        public async Task<AddRestaurant> AddResta(AddRestaurant resta)
        {
            try
            {
                await _dbContext.Restaurants.AddAsync(new Restaurant
                {
                    Name = resta.Name,
                    Address = resta.Address,
                    PhoneNumber = resta.PhoneNumber,
                    OpeningHours = resta.OpeningHours,
                    Capacity = resta.Capacity
                });
                await _dbContext.SaveChangesAsync();
                return resta;
            }
            catch (DbUpdateException ex)
            {
                throw new RestaurantException("Error occurred while adding the restaurant.", ex);
            }
            catch (Exception ex)
            {
                throw new RestaurantException("An unexpected error occurred while adding the restaurant.", ex);
            }

        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            return await _dbContext.Restaurants.ToListAsync();
        }

        public async Task<Restaurant> GetRestaurantById(int id)
        {
            try
            {
                Restaurant? rest=null;
                try
                {
                    rest = await _dbContext.Restaurants.FindAsync(id);
                }
                catch (Exception ex)
                {
                    throw new RestaurantException("An unexpected error occurred while retrieving the restaurant.", ex);
                }
                return rest;
            }
            catch (Exception ex)
            {
                throw new RestaurantException($"Error occurred while retrieving the restaurant with ID {id}.", ex);
            }
        }

        public async Task<Restaurant> DeleteRestaurant(int rid)
        {

            try
            {
                var exrest = await _dbContext.Restaurants.FindAsync(rid);
                _dbContext.Restaurants.Remove(exrest);
                await _dbContext.SaveChangesAsync();
                return exrest;
            }
            catch (DbUpdateException ex)
            {
                throw new RestaurantException("Error occurred while deleting the restaurant.", ex);
            }
            catch (Exception ex)
            {
                throw new RestaurantException("An unexpected error occurred while deleting the restaurant.", ex);
            }
        }

        public async Task<Restaurant> UpdateRestInformation(AddRestaurant rest, int i)
        {
            try
            {
                var exrest = await _dbContext.Restaurants.FindAsync(i);
                exrest.Name = rest.Name;
                exrest.Address = rest.Address;
                exrest.PhoneNumber = rest.PhoneNumber;
                exrest.OpeningHours = rest.OpeningHours;
                exrest.Capacity = rest.Capacity;
                exrest.UpdatedAt = DateTime.UtcNow;
                _dbContext.Restaurants.Update(exrest);
                await _dbContext.SaveChangesAsync();
                return exrest;
            }
            catch (DbUpdateException ex)
            {
                throw new RestaurantException("Error occurred while updating the restaurant.", ex);
            }
            catch (Exception ex)
            {
                throw new RestaurantException("An unexpected error occurred while updating the restaurant.", ex);
            }
        }
    }
}
