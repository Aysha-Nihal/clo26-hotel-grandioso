// HotelRepository.cs — Implements those operations using EF Core and LINQ.

using Microsoft.EntityFrameworkCore;

public class HotelRepository : IHotelRepository
{
    private readonly HotelDbContext _context;    // Constructor injection

    // Constructor injection allows the repository to use an existing database context.
    public HotelRepository(HotelDbContext context)
    {
        _context = context;
    }

    public List<Room> GetAllRooms()
    {
        return _context.Rooms                   // LINQ and Include()
            .Include(r => r.RoomType)           // Loads the associated room type
            .OrderBy(r => r.RoomNumber)         // Sorts rooms by room number
            .ToList();                          // Executes the query and returns the results
    }

    public List<Booking> GetAllBookings()
    {
        return _context.Bookings
            .Include(b => b.Guest)
            .Include(b => b.Room)
            .OrderBy(b => b.CheckIn)
            .ToList();
    }

    public Room? GetRoomByNumber(int roomNumber)
    {
        return _context.Rooms
            .Include(r => r.RoomType)
            .FirstOrDefault(r => r.RoomNumber == roomNumber);
    }

    public Guest? GetGuestByName(string name)
    {
        return _context.Guests
            .FirstOrDefault(g => g.Name == name);
    }

    public void AddGuest(Guest guest)
    {
        _context.Guests.Add(guest);
        _context.SaveChanges();
    }

    public void AddBooking(Booking booking)
    {
        _context.Bookings.Add(booking);
        _context.SaveChanges();
    }

    public void RemoveBooking(Booking booking)
    {
        _context.Bookings.Remove(booking);
        _context.SaveChanges();
    }
}
