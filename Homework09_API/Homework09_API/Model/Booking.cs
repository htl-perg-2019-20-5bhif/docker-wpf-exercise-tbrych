using System;
using System.ComponentModel.DataAnnotations;

namespace Homework09_API.Model
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
