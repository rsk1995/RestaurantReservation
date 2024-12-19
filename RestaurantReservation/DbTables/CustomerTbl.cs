
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.DbTables
{
    public class CustomerTbl
    {
        [Key] // Primary Key
        public int CId { get; set; }


        public string? Name { get; set; }


        public string? Address { get; set; }


        public string? PhoneNumber { get; set; }
    }
}
