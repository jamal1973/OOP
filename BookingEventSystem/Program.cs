using System;

namespace BookingEventSystem
{
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
