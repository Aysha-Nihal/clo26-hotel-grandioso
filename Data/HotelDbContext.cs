// HotelDbContext.cs — Manages database access.

using Microsoft.EntityFrameworkCore;

public class HotelDbContext : DbContext  // DbContext gives our class database-management functionality.
{
    // Each DbSet represents an entity collection in the database.
    public DbSet<Room> Rooms => Set<Room>();   // Hotel's rooms to EF Core
    public DbSet<RoomType> RoomTypes => Set<RoomType>();
    public DbSet<Guest> Guests => Set<Guest>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Treatment> Treatments => Set<Treatment>();
    public DbSet<SpaBooking> SpaBookings => Set<SpaBooking>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            // Read the connection string from the environment to keep configuration outside the source code.
            string? connectionString =
                Environment.GetEnvironmentVariable("HOTEL_DB");

            // Fail early to avoid starting the application without a valid database configuration.
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Environment variable HOTEL_DB is missing.");
            }

            options.UseSqlite(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Each room number must identify exactly one room in the hotel.
        modelBuilder.Entity<Room>()
            .HasIndex(r => r.RoomNumber)
            .IsUnique();
    }
}


// EF Core is an Object-Relational Mapper (ORM). It translates operations on C# objects into database operations.

// SQLite is the database engine.It stores our information in a file named hotel.db.

// Together, they allow the application to save and retrieve data even after the program closes.