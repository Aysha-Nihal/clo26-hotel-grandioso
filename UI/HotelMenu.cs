public class HotelMenu
{
    private readonly IHotelRepository _repository;
    private readonly BookingService _bookingService;
    private readonly SpaService _spaService;

    public HotelMenu(
        IHotelRepository repository,
        BookingService bookingService,
        SpaService spaService)
    {
        _repository = repository;
        _bookingService = bookingService;
        _spaService = spaService;
    }

    public void Run()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n========== HOTEL GRANDIOSO ==========");
            Console.WriteLine("1. Show all rooms");
            Console.WriteLine("2. Show available rooms");
            Console.WriteLine("3. Book a room");
            Console.WriteLine("4. Cancel a booking");
            Console.WriteLine("5. Show all bookings");
            Console.WriteLine("6. Show spa treatments");
            Console.WriteLine("7. Book a spa treatment");
            Console.WriteLine("0. Exit");
            Console.Write("\nChoose an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowAllRooms();
                    break;

                case "2":
                    ShowAvailableRooms();
                    break;

                case "3":
                    BookRoom();
                    break;

                case "4":
                    CancelBooking();
                    break;

                case "5":
                    ShowAllBookings();
                    break;

                case "6":
                    ShowSpaTreatments();
                    break;

                case "7":
                    BookSpaTreatment();
                    break;

                case "0":
                    running = false;
                    Console.WriteLine("Thank you for using Hotel Grandioso!");
                    break;

                default:
                    Console.WriteLine("Invalid option or feature not implemented yet.");
                    break;
            }
        }
    }

    private void ShowAllRooms()                          // CASE 1                                              
    {
        Console.WriteLine("\n=== All Hotel Rooms ===");

        List<Room> rooms = _repository.GetAllRooms();

        foreach (Room room in rooms)
        {
            Console.WriteLine(
                $"{room.RoomNumber} - {room.RoomType.Name} - " +
                $"{room.SizeM2} m² - {room.PricePerNight:N0} kr/night");
        }
    }


    private void ShowAvailableRooms()                   // CASE 2
    {
        Console.WriteLine("\n=== Check Available Rooms ===");

        // Read check-in date.
        Console.Write("Enter check-in date (yyyy-MM-dd): ");
        string? checkInInput = Console.ReadLine();

        // Read check-out date.
        Console.Write("Enter check-out date (yyyy-MM-dd): ");
        string? checkOutInput = Console.ReadLine();

        // Validate both date inputs.
        bool validCheckIn = DateTime.TryParseExact(
            checkInInput,
            "yyyy-MM-dd",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out DateTime checkIn);

        bool validCheckOut = DateTime.TryParseExact(
            checkOutInput,
            "yyyy-MM-dd",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out DateTime checkOut);

        if (!validCheckIn || !validCheckOut)
        {
            Console.WriteLine("Invalid date format. Use yyyy-MM-dd.");
            return;
        }

        if (!_bookingService.AreDatesValid(checkIn, checkOut))
        {
            Console.WriteLine("Check-out must be later than check-in.");
            return;
        }

        // Retrieve available rooms using the booking service.
        List<Room> availableRooms =
            _bookingService.GetAvailableRooms(checkIn, checkOut);

        Console.WriteLine($"\nAvailable rooms: {availableRooms.Count}");

        foreach (Room room in availableRooms)
        {
            decimal totalPrice =
                _bookingService.CalculateTotalPrice(
                    room, checkIn, checkOut);

            Console.WriteLine(
                $"{room.RoomNumber} - {room.RoomType.Name} - " +
                $"{room.SizeM2} m² - " +
                $"{room.PricePerNight:N0} kr/night - " +
                $"Total: {totalPrice:N0} kr");
        }

        if (availableRooms.Count == 0)
        {
            Console.WriteLine("No rooms available for these dates.");
        }
    }

    private void BookRoom()                                // CASE 3
    {
        Console.WriteLine("\n=== Book a Room ===");

        // Read and validate guest name.
        Console.Write("Enter guest name: ");
        string guestName = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(guestName))
        {
            Console.WriteLine("Guest name cannot be empty.");
            return;
        }

        // Read and validate room number.
        Console.Write("Enter room number: ");

        if (!int.TryParse(Console.ReadLine(), out int roomNumber))
        {
            Console.WriteLine("Invalid room number.");
            return;
        }

        Room? room = _repository.GetRoomByNumber(roomNumber);

        if (room == null)
        {
            Console.WriteLine("Room not found.");
            return;
        }

        // Read and validate booking dates.
        Console.Write("Enter check-in date (yyyy-MM-dd): ");
        string? checkInInput = Console.ReadLine();

        Console.Write("Enter check-out date (yyyy-MM-dd): ");
        string? checkOutInput = Console.ReadLine();

        bool validCheckIn = DateTime.TryParseExact(
            checkInInput,
            "yyyy-MM-dd",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out DateTime checkIn);

        bool validCheckOut = DateTime.TryParseExact(
            checkOutInput,
            "yyyy-MM-dd",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out DateTime checkOut);

        if (!validCheckIn || !validCheckOut)
        {
            Console.WriteLine("Invalid date format. Use yyyy-MM-dd.");
            return;
        }

        if (!_bookingService.AreDatesValid(checkIn, checkOut))
        {
            Console.WriteLine("Check-out must be later than check-in.");
            return;
        }

        // Avoid creating a guest when the room is already booked.
        if (!_bookingService.IsRoomAvailable(room.Id, checkIn, checkOut))
        {
            Console.WriteLine("Room is not available for these dates.");
            return;
        }

        // Find existing guest or create a new guest.
        Guest? guest = _repository.GetGuestByName(guestName);

        if (guest == null)
        {
            guest = new Guest { Name = guestName };
            _repository.AddGuest(guest);
        }

        // Create booking through the booking service.
        bool booked = _bookingService.CreateBooking(
            roomNumber,
            guest.Id,
            checkIn,
            checkOut);

        if (booked)
        {
            decimal totalPrice = _bookingService.CalculateTotalPrice(
                room, checkIn, checkOut);

            Console.WriteLine("\nBooking confirmed!");
            Console.WriteLine($"Guest: {guest.Name}");
            Console.WriteLine($"Room: {room.RoomNumber}");
            Console.WriteLine($"Check-in: {checkIn:yyyy-MM-dd}");
            Console.WriteLine($"Check-out: {checkOut:yyyy-MM-dd}");
            Console.WriteLine($"Total price: {totalPrice:N0} kr");
        }
        else
        {
            Console.WriteLine("Booking failed. The room may no longer be available.");
        }
    }

    private void CancelBooking()                     // CASE 4
    {
        Console.WriteLine("\n=== Cancel a Booking ===");

        // Display existing bookings so the user can identify the booking ID.
        List<Booking> bookings = _repository.GetAllBookings();

        if (bookings.Count == 0)
        {
            Console.WriteLine("There are no bookings to cancel.");
            return;
        }

        foreach (Booking booking in bookings)
        {
            Console.WriteLine(
                $"ID: {booking.Id} - Guest: {booking.Guest.Name} - " +
                $"Room: {booking.Room.RoomNumber} - " +
                $"{booking.CheckIn:yyyy-MM-dd} to {booking.CheckOut:yyyy-MM-dd}");
        }

        // Read the booking ID safely.
        Console.Write("\nEnter booking ID to cancel (0 to return): ");

        if (!int.TryParse(Console.ReadLine(), out int bookingId))
        {
            Console.WriteLine("Invalid booking ID.");
            return;
        }

        if (bookingId == 0)
        {
            Console.WriteLine("Cancellation aborted.");
            return;
        }

        // Confirm before deleting the reservation.
        Console.Write($"Are you sure you want to cancel booking {bookingId}? (y/n): ");
        string? confirmation = Console.ReadLine();

        if (!string.Equals(confirmation, "y",
            StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Cancellation aborted.");
            return;
        }

        bool cancelled = _bookingService.CancelBooking(bookingId);

        Console.WriteLine(cancelled
            ? "Booking cancelled successfully!"
            : "Booking not found.");
    }

    private void ShowAllBookings()               // CASE 5
    {
        Console.WriteLine("\n=== All Hotel Bookings ===");

        // Retrieve bookings with related guest and room information.
        List<Booking> bookings = _repository.GetAllBookings();

        if (bookings.Count == 0)
        {
            Console.WriteLine("No bookings found.");
            return;
        }

        foreach (Booking booking in bookings)
        {
            // Calculate the number of nights and total booking price.
            int nights = (booking.CheckOut.Date -
                          booking.CheckIn.Date).Days;

            decimal totalPrice = _bookingService.CalculateTotalPrice(
                booking.Room,
                booking.CheckIn,
                booking.CheckOut);

            Console.WriteLine($"\nBooking ID: {booking.Id}");
            Console.WriteLine($"Guest: {booking.Guest.Name}");
            Console.WriteLine($"Room: {booking.Room.RoomNumber}");
            Console.WriteLine($"Check-in: {booking.CheckIn:yyyy-MM-dd}");
            Console.WriteLine($"Check-out: {booking.CheckOut:yyyy-MM-dd}");
            Console.WriteLine($"Nights: {nights}");
            Console.WriteLine($"Total price: {totalPrice:N0} kr");
        }

        Console.WriteLine($"\nTotal bookings: {bookings.Count}");
    }


    private void ShowSpaTreatments()               // CASE 6
    {
        Console.WriteLine("\n=== Spa Treatments ===");

        // Retrieve spa treatments through the service.
        List<Treatment> treatments = _spaService.GetTreatments();

        if (treatments.Count == 0)
        {
            Console.WriteLine("No spa treatments available.");
            return;
        }

        foreach (Treatment treatment in treatments)
        {
            Console.WriteLine(
                $"{treatment.Id} - {treatment.Name} - " +
                $"{treatment.DurationMinutes} min - " +
                $"{treatment.Price:N0} kr");
        }

        Console.WriteLine($"\nTotal treatments: {treatments.Count}");
    }

    private void BookSpaTreatment()               // CASE 7
    {
        Console.WriteLine("\n=== Book a Spa Treatment ===");

        // Display available treatments before asking the user to select one.
        List<Treatment> treatments = _spaService.GetTreatments();

        if (treatments.Count == 0)
        {
            Console.WriteLine("No spa treatments available.");
            return;
        }

        foreach (Treatment treatment in treatments)
        {
            Console.WriteLine(
                $"{treatment.Id} - {treatment.Name} - " +
                $"{treatment.DurationMinutes} min - " +
                $"{treatment.Price:N0} kr");
        }

        // Read guest name.
        Console.Write("\nEnter guest name: ");
        string guestName = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(guestName))
        {
            Console.WriteLine("Guest name cannot be empty.");
            return;
        }

        // Read treatment ID.
        Console.Write("Enter treatment ID: ");

        if (!int.TryParse(Console.ReadLine(), out int treatmentId))
        {
            Console.WriteLine("Invalid treatment ID.");
            return;
        }

        Treatment? selectedTreatment =
            treatments.FirstOrDefault(t => t.Id == treatmentId);

        if (selectedTreatment == null)
        {
            Console.WriteLine("Treatment not found.");
            return;
        }

        // Read the appointment date and time.
        Console.Write("Enter appointment (yyyy-MM-dd HH:mm): ");
        string? dateInput = Console.ReadLine();

        bool validDate = DateTime.TryParseExact(
            dateInput,
            "yyyy-MM-dd HH:mm",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out DateTime appointment);

        if (!validDate)
        {
            Console.WriteLine("Invalid date format. Use yyyy-MM-dd HH:mm.");
            return;
        }

        // Find the guest or create a new guest.
        Guest? guest = _repository.GetGuestByName(guestName);

        if (guest == null)
        {
            guest = new Guest { Name = guestName };
            _repository.AddGuest(guest);
        }

        // Save the spa booking through SpaService.
        bool booked = _spaService.BookTreatment(
            guest.Id,
            treatmentId,
            appointment);

        if (booked)
        {
            Console.WriteLine("\nSpa booking confirmed!");
            Console.WriteLine($"Guest: {guest.Name}");
            Console.WriteLine($"Treatment: {selectedTreatment.Name}");
            Console.WriteLine($"Appointment: {appointment:yyyy-MM-dd HH:mm}");
            Console.WriteLine($"Duration: {selectedTreatment.DurationMinutes} minutes");
            Console.WriteLine($"Price: {selectedTreatment.Price:N0} kr");
        }
        else
        {
            Console.WriteLine("Spa booking failed.");
        }
    }

}
