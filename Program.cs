

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
