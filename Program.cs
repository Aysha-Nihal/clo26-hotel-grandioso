

// TODO: Skapa databasen med EF Core 

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


