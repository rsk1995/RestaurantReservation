using Microsoft.EntityFrameworkCore;
using RestaurantReservation.DbTables;

namespace RestaurantReservation
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<CustomerTbl> Customers { get; set; }

        public DbSet<TableCapacity> TableStatus { get; set; }

        public DbSet<ReservedBookings> ReservedBookings { get; set; }
    }
}
