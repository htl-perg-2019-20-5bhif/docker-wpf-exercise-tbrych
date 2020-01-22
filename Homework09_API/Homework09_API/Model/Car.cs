using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Homework09_API.Model
{
    public class Car
    {
        public int CarId { get; set; }

        [Required]
        [MaxLength(25)]
        public string Make { get; set; }

        [Required]
        [MaxLength(75)]
        public string Model { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
