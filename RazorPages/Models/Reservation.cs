
    public class Reservation
    {
        public int Id { get; set; } // Assuming you have an Id property for the Reservation

        public Room? Room { get; set; } // Reference to the Room class, marked as nullable

        public DateTime DateTime { get; set; } // Date and time of the reservation

        public string? ReservedBy { get; set; } // Name of the person who made the reservation, marked as nullable
    }