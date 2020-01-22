using System;
using System.Text.Json.Serialization;

namespace Homework09_WPF.Model
{
    public class Booking
    {
        [JsonPropertyName("bookingId")]
        public int BookingId { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }
}
