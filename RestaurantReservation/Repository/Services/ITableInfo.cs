using RestaurantReservation.DbTables;
using RestaurantReservation.DTO;

namespace RestaurantReservation.Repository.Services
{
    public interface ITableInfo
    {
        Task<bool> AddTableDetails(TableDTO tbl);

        Task<IEnumerable<TableCapacity>> GetAllTableInfo();
        Task<TableCapacity> GetTableInfoById(int id);

        Task<IEnumerable<TableCapacity>> GetAvailableTables();

        Task<IEnumerable<TableCapacity>> GetReservedTables();

        Task<TableCapacity> DeleteTableInfo(int tid);

        Task<TableCapacity> UpdateTableInfo(TableDTO tbl, int i);
    }
}
