using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.DTO
{
    public class AddRestaurant
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100)]
        public string? Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200)]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Phone is required.")]
        [Phone]
        [Length(10,10)]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Opening timings with days is required.")]
        public string? OpeningHours { get; set; } // Store as JSON or string, e.g., { "Mon-Fri": "9:00 AM - 9:00 PM" }
        [Required(ErrorMessage = "Restaurant customer capacity is required.")]
        [Range(1,10000, ErrorMessage = "Capacity should between 1 to 10k")]
        public int Capacity { get; set; } 
    }
}
