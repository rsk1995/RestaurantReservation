using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.DbTables;
using RestaurantReservation.Repository.Services;
using System.IdentityModel.Tokens.Jwt;

namespace RestaurantReservation.Repository.ServiceImplementation
{
    public class LogInManagement
    {
        private readonly RestaurantDbContext _dbContext;
        public LogInManagement(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public Task<ActionResult<LOGIN>> GetEmployeeInfoByID_WithJWTToken(int id, string token)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    // Check if the token is valid
        //    if (!handler.CanReadToken(token))
        //    {
        //        Console.WriteLine("Invalid token");
        //        return Ok();
        //    }
        //    // Read the token
        //    var jwtToken = handler.ReadJwtToken(token);
        //}
    }
}
