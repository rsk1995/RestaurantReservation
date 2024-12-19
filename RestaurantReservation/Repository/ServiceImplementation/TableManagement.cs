using Microsoft.EntityFrameworkCore;
using RestaurantReservation.DbTables;
using RestaurantReservation.DTO;
using RestaurantReservation.Exceptions;
using RestaurantReservation.Repository.Services;
using System.Reflection;
using System.Security.Cryptography;

namespace RestaurantReservation.Repository.ServiceImplementation
{
    public class TableManagement : ITableInfo
    {
        private readonly RestaurantDbContext _dbContext;
        public TableManagement(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddTableDetails(TableDTO tbl)
        {
            try
            {
                await _dbContext.TableStatus.AddAsync(new TableCapacity
                {
                    TCapacity = tbl.TCapacity,
                });
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new TableException("Error occurred while adding the table.", ex);
            }
            catch (Exception ex)
            {
                throw new TableException("An unexpected error occurred while adding the table.", ex);
            }
        }

        public async Task<IEnumerable<TableCapacity>> GetAllTableInfo()
        {
            return await _dbContext.TableStatus.ToListAsync();
        }

        public async Task<IEnumerable<TableCapacity>> GetAvailableTables()
        {
            try
            {
                try
                {
                    return await _dbContext.TableStatus.Where(p => p.Status == "Available").ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new TableException("An unexpected error occurred while retrieving the available table information.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new TableException($"Error occurred while retrieving the available table information.", ex);
            }
        }

        public async Task<IEnumerable<TableCapacity>> GetReservedTables()
        {
            try
            {
                try
                {
                    return await _dbContext.TableStatus.Where(p => p.Status == "Reserved").ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new TableException("An unexpected error occurred while retrieving the reserved table information.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new TableException($"Error occurred while retrieving the reserved table information.", ex);
            }
        }
        public async Task<TableCapacity> GetTableInfoById(int id)
        {
            try
            {
                TableCapacity? tbl = null;
                try
                {
                    tbl = await _dbContext.TableStatus.FindAsync(id);
                }
                catch (Exception ex)
                {
                    throw new TableException("An unexpected error occurred while retrieving the table information.", ex);
                }
                return tbl;
            }
            catch (Exception ex)
            {
                throw new TableException($"Error occurred while retrieving the table information with ID {id}.", ex);
            }
        }

        public async Task<TableCapacity> DeleteTableInfo(int tid)
        {

            try
            {
                var extbl = await _dbContext.TableStatus.FindAsync(tid);
                _dbContext.TableStatus.Remove(extbl);
                await _dbContext.SaveChangesAsync();
                return extbl;
            }
            catch (DbUpdateException ex)
            {
                throw new TableException("Error occurred while deleting the table.", ex);
            }
            catch (Exception ex)
            {
                throw new TableException("An unexpected error occurred while deleting the table.", ex);
            }
        }

        public async Task<TableCapacity> UpdateTableInfo(TableDTO tbl, int i)
        {
            try
            {
                var extbl = await _dbContext.TableStatus.FindAsync(i);
                extbl.TCapacity = tbl.TCapacity;
                _dbContext.TableStatus.Update(extbl);
                await _dbContext.SaveChangesAsync();
                return extbl;
            }
            catch (DbUpdateException ex)
            {
                throw new TableException("Error occurred while updating the restaurant.", ex);
            }
            catch (Exception ex)
            {
                throw new TableException("An unexpected error occurred while updating the table.", ex);
            }
        }

    }
}
