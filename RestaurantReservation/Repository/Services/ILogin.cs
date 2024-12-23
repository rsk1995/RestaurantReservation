using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.DbTables;

namespace RestaurantReservation.Repository.Services
{
    public interface ILogin
    {
        public Task<ActionResult<LOGIN>> GetEmployeeInfoByID_WithJWTToken(int id, string token);
    }
}
