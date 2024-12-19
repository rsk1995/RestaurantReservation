using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.DTO
{
    public class ReserveBookingDTO
    {
        [Required(ErrorMessage = "Date is required.")]  
        public DateTime EventDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "customer id required.")]
        public int CId { get; set; }

        [Required(ErrorMessage = "Table id required.")]
        public int TId { get; set; }
    }
}
