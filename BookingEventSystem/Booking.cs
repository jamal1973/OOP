namespace BookingEventSystem
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string CustomerName { get; set; }
        public int NumTickets { get; set; }
        public int EventId { get; set; }

        public Booking(int bookingId, string customerName, int numTickets, int eventId)
        {
            BookingId = bookingId;
            CustomerName = customerName;
            NumTickets = numTickets;
            EventId = eventId;
        }
    }
}
