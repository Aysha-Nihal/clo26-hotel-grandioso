
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

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // The connection is configurable without changing source code.
            string connectionString =
                Environment.GetEnvironmentVariable(
                    "HOTEL_DB_CONNECTION")
                ?? "Data Source=hotel.db";

            optionsBuilder.UseSqlite(connectionString); // SQLite connection ie EF Core to use SQLite.
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Room numbers must be unique across the hotel.
        modelBuilder.Entity<Room>()
            .HasIndex(r => r.RoomNumber)
            .IsUnique();
    }
}
