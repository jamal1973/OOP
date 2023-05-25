using System;
using System.Collections.Generic;

namespace BookingEventSystem
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Capacity { get; set; }
        public List<Booking> Bookings { get; set; }

        public Event(int id, string name, DateTime date, int capacity)
        {
            Id = id;
            Name = name;
            Date = date;
            Capacity = capacity;
            Bookings = new List<Booking>();
        }

        public bool IsAvailable(int numTickets)
        {
            return Capacity - GetTotalTicketsBooked() >= numTickets;
        }

        public int GetTotalTicketsBooked()
        {
            int totalTicketsBooked = 0;
            foreach (var booking in Bookings)
            {
                totalTicketsBooked += booking.NumTickets;
            }
            return totalTicketsBooked;
        }

        public void AddBooking(Booking booking)
        {
            if (IsAvailable(booking.NumTickets))
            {
                Bookings.Add(booking);
                Console.WriteLine($"Booking created for event {Name}");
            }
            else
            {
                Console.WriteLine($"Event {Name} does not have enough available seats for {booking.NumTickets} tickets");
            }
        }

        public void CancelBooking(int bookingId)
        {
            Booking booking = FindBooking(bookingId);
            if (booking != null)
            {
                Bookings.Remove(booking);
                Console.WriteLine($"Booking {bookingId} canceled for event {Name}");
            }
            else
            {
                Console.WriteLine($"Booking {bookingId} not found for event {Name}");
            }
        }

        public void DisplayAvailableTickets()
        {
            int availableTickets = Capacity - GetTotalTicketsBooked();
            Console.WriteLine($"Dostępnych biletów na wydarzenie '{Name}': {availableTickets}");
        }

        private Booking FindBooking(int bookingId)
        {
            foreach (var booking in Bookings)
            {
                if (booking.BookingId == bookingId)
                {
                    return booking;
                }
            }
            return null;
        }
    }

}
