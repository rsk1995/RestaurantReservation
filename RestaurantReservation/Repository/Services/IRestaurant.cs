using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.DbTables;
using RestaurantReservation.DTO;
using static System.Reflection.Metadata.BlobBuilder;

namespace RestaurantReservation.Repository.Interfaces
{
    public interface IRestaurant
    {
        Task<AddRestaurant> AddResta(AddRestaurant restaurant);

        Task<IEnumerable<Restaurant>> GetAllRestaurants();
        Task<Restaurant> GetRestaurantById(int id);

        Task<Restaurant> DeleteRestaurant(int rid);

        Task<Restaurant> UpdateRestInformation(AddRestaurant rest, int i);
    }
}
