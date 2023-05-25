using System;
using System.Collections.Generic;

namespace BookingEventSystem
{
    public class BookingSystem
    {
        private List<Event> events;
        private List<Booking> bookings;
        private int nextEventId;
        private int nextBookingId;

        public BookingSystem()
        {
            events = new List<Event>();
            bookings = new List<Booking>();
            nextEventId = 1;
            nextBookingId = 1;
        }

        public void AddEvent(string eventName, DateTime eventDate, int eventCapacity)
        {
            Event evnt = new Event(nextEventId, eventName, eventDate, eventCapacity);
            events.Add(evnt);
            nextEventId++;
            Console.WriteLine($"Dodano wydarzenie: '{eventName}'");
        }

        public void MakeBooking(string customerName)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Dostępne wydarzenia:");
            Console.ResetColor();

            bool hasAvailableEvents = false;

            foreach (var evnt in events)
            {
                if (evnt.IsAvailable(1))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"ID: {evnt.Id}, Nazwa: {evnt.Name}, Data: {evnt.Date.ToString("yyyy-MM-dd")}, Bilety: {evnt.Capacity}");
                    hasAvailableEvents = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ID: {evnt.Id}, Nazwa: {evnt.Name}, Data: {evnt.Date.ToString("yyyy-MM-dd")}, Bilety: {evnt.Capacity}");
                }
                Console.ResetColor();
            }

            if (!hasAvailableEvents)
            {
                Console.WriteLine("Brak dostępnych wydarzeń.");
                return;
            }

            Console.Write("Podaj ID wydarzenia: ");
            int eventId = int.Parse(Console.ReadLine());

            Event selectedEvent = FindEvent(eventId);

            if (selectedEvent != null)
            {
                Console.Write("Podaj ilość biletów: ");
                int numTickets = int.Parse(Console.ReadLine());

                if (selectedEvent.IsAvailable(numTickets))
                {
                    Booking booking = new Booking(nextBookingId, customerName, numTickets, eventId);
                    selectedEvent.AddBooking(booking);
                    bookings.Add(booking);
                    Console.WriteLine($"Utworzyłem rezerwacje o ID: {nextBookingId}");
                    nextBookingId++;
                }
                else
                {
                    Console.WriteLine($"Event {selectedEvent.Name} does not have enough available seats for {numTickets} tickets");
                }
            }
            else
            {
                Console.WriteLine($"Wydarzenie o ID {eventId} nie znalezione");
            }
        }

        public void CancelBooking(int bookingId)
        {
            Booking booking = FindBooking(bookingId);
            if (booking != null)
            {
                Event evnt = FindEvent(booking.EventId);
                evnt.CancelBooking(bookingId);
                bookings.Remove(booking);
            }
            else
            {
                Console.WriteLine($"Rezerwacja o ID {bookingId} nie znaleziona");
            }
        }

        public void DisplayEvents()
        {
            Console.WriteLine("Dostępne wydarzenia:");
            foreach (var evnt in events)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"ID: {evnt.Id}, Nazwa: {evnt.Name}, Data: {evnt.Date.ToString("yyyy-MM-dd")}, Bilety: {evnt.Capacity}");
                Console.ResetColor();
            }
        }

        public void DisplayBookings()
        {
            Console.WriteLine("Wszystkie Rezerwacje:");
            foreach (var booking in bookings)
            {
                Event evnt = FindEvent(booking.EventId);
                Console.WriteLine($"ID rezerwacji : {booking.BookingId}, Wydarzenie: {evnt.Name}, Data: {evnt.Date.ToString("yyyy-MM-dd")}, Zamawiający: {booking.CustomerName}, Bilet/y: {booking.NumTickets}");
            }
        }

        public void DisplayEventBookings(int eventId)
        {
            Event evnt = FindEvent(eventId);
            if (evnt != null)
            {
                Console.WriteLine($"Rezerwacje na wydarzenia '{evnt.Name}':");
                foreach (var booking in evnt.Bookings)
                {
                    Console.WriteLine($"Booking ID: {booking.BookingId}, Zamawiający: {booking.CustomerName}, Bilet/y: {booking.NumTickets}");
                }
            }
            else
            {
                Console.WriteLine($"Wydarzenie o ID {eventId} nie znalezione");
            }
        }

        public void DisplayAvailableTickets()
        {
            Console.WriteLine("Dostępne bilety na wydarzenie:");
            foreach (var evnt in events)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"Wydarzenie '{evnt.Name}': ");
                Console.ResetColor();
                evnt.DisplayAvailableTickets();
            }
        }

        private Event FindEvent(int eventId)
        {
            foreach (var evnt in events)
            {
                if (evnt.Id == eventId)
                {
                    return evnt;
                }
            }
            return null;
        }

        private Booking FindBooking(int bookingId)
        {
            foreach (var booking in bookings)
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
