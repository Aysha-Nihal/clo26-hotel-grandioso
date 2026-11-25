

// TODO: Skapa databasen med EF Core (se Data/README.md)

// TODO: Starta menyn (Boka rum, Avboka, Visa bokningar, Visa lediga rum, Avsluta)


using Microsoft.EntityFrameworkCore;

Console.WriteLine("=== Hotel Grandioso ===");

// Open the database 
using var db = new HotelDbContext();

Console.WriteLine(
    $"Database path: {Path.GetFullPath(db.Database.GetDbConnection().DataSource)}");

// Apply migrations
db.Database.Migrate();

// Insert the initial hotel data if it does not exist.
DatabaseSeeder.Seed(db);

// Verify the seeded records using LINQ.
Console.WriteLine($"Room types: {db.RoomTypes.Count()}");
Console.WriteLine($"Rooms: {db.Rooms.Count()}");
Console.WriteLine($"Spa treatments: {db.Treatments.Count()}");

Console.WriteLine("Database initialization completed!");

// Create the repository using the existing database context.
IHotelRepository repository = new HotelRepository(db);          // Repository initialization

// Retrieve all rooms through the repository.
Console.WriteLine("\nAvailable hotel rooms:");

// Retrieving rooms using the repository
List<Room> rooms = repository.GetAllRooms();                    

// Display room details, including room type and nightly price.
foreach (Room room in rooms)
{
    Console.WriteLine(
        $"{room.RoomNumber} - {room.RoomType.Name} - " +
        $"{room.SizeM2} m² - {room.PricePerNight} kr/night");
}

Console.WriteLine($"\nTotal rooms: {rooms.Count}");
