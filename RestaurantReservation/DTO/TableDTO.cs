using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.DTO
{
    public class TableDTO
    {
        [Required(ErrorMessage = "Table capacity required")]
        [Range(1, 10)]
        public int TCapacity { get; set; }
    }
}
