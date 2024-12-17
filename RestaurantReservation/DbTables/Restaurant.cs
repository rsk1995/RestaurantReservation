using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.DbTables
{
    public class Restaurant
    {
        [Key] // Primary Key
        public int Id { get; set; }


        public string? Name { get; set; }


        public string? Address { get; set; }


        public string? PhoneNumber { get; set; }


        public string? OpeningHours { get; set; } // Store as JSON or string, e.g., { "Mon-Fri": "9:00 AM - 9:00 PM" }

        
        public int Capacity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
