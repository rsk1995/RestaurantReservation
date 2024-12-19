 using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.DbTables
{
    public class TableCapacity
    {
        [Key]
        public int TableId { get; set; }
        public int TCapacity { get; set; }
        public string Status { get; set; } = "Available";
    }
}