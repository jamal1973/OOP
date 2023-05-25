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

    class Program
    {
        static void Main(string[] args)
        {
            BookingSystem bookingSystem = new BookingSystem();

            // Dodaj przykładowe wydarzenia
            bookingSystem.AddEvent("Concert", new DateTime(2023, 7, 15), 100);
            bookingSystem.AddEvent("Theater Play", new DateTime(2023, 8, 20), 50);
            bookingSystem.AddEvent("Conference", new DateTime(2023, 9, 10), 200);

            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Booking Event System");
                Console.WriteLine("--------------------");
                Console.ResetColor();
                Console.WriteLine("1. Utwórz Rezerwację");
                Console.WriteLine("2. Usuń Rezerwację");
                Console.WriteLine("3. Wyświetl Rezerwacje");
                Console.WriteLine("4. Wyświetl Wydarzenia");
                Console.WriteLine("5. Wyświetl Zarezerwowane Wydarzenia");
                Console.WriteLine("6. Dodaj Wydarzenie");
                Console.WriteLine("7. Wyświetl Dostępne Bilety");
                Console.WriteLine("8. Wyjście");
                Console.Write("\nWprowadź swój wybór: ");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Podaj imię i Nazwisko: ");
                        string customerName = Console.ReadLine();
                        Console.ResetColor();
                        bookingSystem.MakeBooking(customerName);
                        break;
                    case 2:
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Podaj ID rezerwacji: ");
                        int bookingId = int.Parse(Console.ReadLine());
                        Console.ResetColor();
                        bookingSystem.CancelBooking(bookingId);
                        break;
                    case 4:
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Dostęne wydarzenia:");
                        Console.ResetColor();
                        bookingSystem.DisplayEvents();
                        break;
                    case 3:
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Wszystkie rezerwacje:");
                        Console.ResetColor();
                        bookingSystem.DisplayBookings();
                        break;
                    case 5:
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Podaj ID wydarzenia: ");
                        int eventIdToDisplay = int.Parse(Console.ReadLine());
                        Console.ResetColor();
                        bookingSystem.DisplayEventBookings(eventIdToDisplay);
                        break;
                    case 6:
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Podaj nazwę wydarzenia: ");
                        string eventName = Console.ReadLine();
                        Console.Write("Podaj datę wydarzenia (yyyy-mm-dd): ");
                        DateTime eventDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Podaj ilość biletów: ");
                        int eventCapacity = int.Parse(Console.ReadLine());
                        Console.ResetColor();
                        bookingSystem.AddEvent(eventName, eventDate, eventCapacity);
                        break;
                    case 7:
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Dostępne bilety na wydarzenie:");
                        Console.ResetColor();
                        bookingSystem.DisplayAvailableTickets();
                        break;
                    case 8:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("\nBłędny wybór. Spróbuj jeszcze raz.");
                        break;
                }

                Console.WriteLine("\nNaciśnij dowolny przycisk...");
                Console.ReadKey();
            }
        }
    }
}
