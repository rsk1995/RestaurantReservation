using Microsoft.EntityFrameworkCore;
using RestaurantReservation.DbTables;
using RestaurantReservation.DTO;
using RestaurantReservation.Exceptions;
using RestaurantReservation.Repository.Services;

namespace RestaurantReservation.Repository.ServiceImplementation
{
    public class ReserveBookingManagement : IReserveBooking
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly ITableInfo _table;
        public ReserveBookingManagement(RestaurantDbContext dbContext, ITableInfo table)
        {
            _dbContext = dbContext;
            _table = table;
        }
        public async Task<bool> BookTable(ReserveBookingDTO BookInfo)
        {
            try
            {
                TableCapacity extbl = await _table.GetTableInfoById(BookInfo.TId);
                if (extbl.Status == "Available")
                {
                    await _dbContext.ReservedBookings.AddAsync(new ReservedBookings
                    {
                        EventDate = BookInfo.EventDate,
                        CId = BookInfo.CId,
                        TId = BookInfo.TId
                    });
                    extbl.Status = "Reserved";
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
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

        public async Task<ReservedBookings> GetReseredTableInfo(int tblId)
        {
            try
            {
                ReservedBookings? exbook = null;
                try
                {
                    /*var tableHistory = await _dbContext.ReservedBookings.Where(p=>p.TId == tblId).ToListAsync();
                    exbook = await _dbContext.ReservedBookings.Join(
                            TableCapacity,
                            TId =>)*/
                         

                }
                catch (Exception ex)
                {
                    throw new RestaurantException("An unexpected error occurred while retrieving the table.", ex);
                }
                return exbook;
            }
            catch (Exception ex)
            {
                throw new RestaurantException($"Error occurred while retrieving the table with ID {tblId}.", ex);
            }
        }
    }
}
