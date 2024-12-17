using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.DTO
{
    public class CustomerDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100)]
        public string? Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [Phone]
        [Length(10, 10)]
        public string? PhoneNumber { get; set; }
    }
}
