

// TODO: Skapa databasen med EF Core (se Data/README.md)

// TODO: Starta menyn (Boka rum, Avboka, Visa bokningar, Visa lediga rum, Avsluta)


//using Microsoft.EntityFrameworkCore;

//Console.WriteLine("=== Hotel Grandioso ===");

//// Open the database connection.
//using var db = new HotelDbContext();

//// Apply any pending migrations.
//db.Database.Migrate();

//// Insert the initial hotel data if necessary.
//DatabaseSeeder.Seed(db);

//// Create the repository for database operations.
//IHotelRepository repository = new HotelRepository(db);

//// Create the booking service for business logic.
//BookingService bookingService = new BookingService(repository);

//// Display all hotel rooms using the repository.
//Console.WriteLine("\n=== Hotel Rooms ===");

//List<Room> rooms = repository.GetAllRooms();

//foreach (Room room in rooms)
//{
//    Console.WriteLine(
//        $"{room.RoomNumber} - {room.RoomType.Name} - " +
//        $"{room.SizeM2} m² - {room.PricePerNight:N0} kr/night");
//}

//Console.WriteLine($"\nTotal rooms: {rooms.Count}");

//// TODO: Start the console menu after implementing booking operations.




//Console.WriteLine("\n=== Spa Booking Test ===");

//// Create the spa service.
//SpaService spaService = new SpaService(repository);

//// Display all treatments.
//List<Treatment> treatments = spaService.GetTreatments();

//foreach (Treatment treatment in treatments)
//{
//    Console.WriteLine(
//        $"{treatment.Id} - {treatment.Name} - " +
//        $"{treatment.DurationMinutes} min - {treatment.Price:N0} kr");
//}

//// Use the existing test guest.
//Guest? guest = repository.GetGuestByName("Anna Svensson");

//if (guest != null && treatments.Count > 0)
//{
//    // Create one spa reservation.
//    bool booked = spaService.BookTreatment(
//        guest.Id,
//        treatments[0].Id,
//        new DateTime(2026, 12, 22, 14, 0, 0));

//    Console.WriteLine($"Spa booking successful: {booked}");
//}
//else
//{
//    Console.WriteLine("Test guest or treatments not found.");
//}



using Microsoft.EntityFrameworkCore;

Console.WriteLine("Starting Hotel Grandioso...");

// Initialize database and seed data.
using var db = new HotelDbContext();

db.Database.Migrate();
DatabaseSeeder.Seed(db);

// Initialize repository and services.
IHotelRepository repository = new HotelRepository(db);

BookingService bookingService = new BookingService(repository);
SpaService spaService = new SpaService(repository);

// Create and start the interactive console menu.
HotelMenu menu = new HotelMenu(
    repository,
    bookingService,
    spaService);

menu.Run();


