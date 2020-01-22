using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Homework09_WPF.Model
{
    public class Car
    {
        [JsonPropertyName("carId")]
        public int CarId { get; set; }

        [JsonPropertyName("make")]
        public string Make { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("bookings")]
        public List<Booking> Bookings { get; set; }
    }
}
