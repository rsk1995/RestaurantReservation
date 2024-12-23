using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.DTO
{
    public class LogInDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
