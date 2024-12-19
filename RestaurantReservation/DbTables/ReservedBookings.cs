using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.DbTables
{
    public class ReservedBookings
    {
        [Key]
        public int BookingID { get; set; }
        public DateTime EventDate { get; set; }
        public int CId { get; set; }
        public int TId { get; set; }
    }
}