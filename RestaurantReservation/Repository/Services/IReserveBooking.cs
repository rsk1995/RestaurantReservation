using RestaurantReservation.DbTables;
using RestaurantReservation.DTO;

namespace RestaurantReservation.Repository.Services
{
    public interface IReserveBooking
    {
        Task<bool> BookTable(ReserveBookingDTO BookInfo);

        Task<ReservedBookings> GetReseredTableInfo(int tblId);
    }
}
