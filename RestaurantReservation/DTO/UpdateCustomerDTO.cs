using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.DTO
{
    public class UpdateCustomerDTO
    {
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [Phone]
        [Length(10, 10)]
        public string? PhoneNumber { get; set; }
    }
}
