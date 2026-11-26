// HotelRepository.cs — Implements those operations using EF Core and LINQ.
//                      Implements database operations using EF Core.

using Microsoft.EntityFrameworkCore;

public class HotelRepository : IHotelRepository
{
    private readonly HotelDbContext _context;    // Constructor injection

    // Constructor injection allows the repository to reuse an existing database context instead of creating its own dependency.
    public HotelRepository(HotelDbContext context)
    {
        _context = context;
    }

    public List<Room> GetAllRooms()             // Room types are loaded together with rooms because the console menu needs to display their names.
    {
        return _context.Rooms                   // LINQ and Include()
            .Include(r => r.RoomType)           // Loads the associated room type
            .OrderBy(r => r.RoomNumber)         // Sorts rooms by room number
            .ToList();                          // Executes the query and returns the results
    }

    public List<Booking> GetAllBookings()       // Guest and room details are needed when displaying bookings.
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

    public void RemoveBooking(Booking booking)
    {
        _context.Bookings.Remove(booking);
        _context.SaveChanges();
    }

    public bool HasOverlappingBooking(
        int roomId,
        DateTime checkIn,
        DateTime checkOut)
    {
        // Both date comparisons are required to detect overlapping reservations while allowing check-in on the previous checkout date.
        return _context.Bookings.Any(b =>
            b.RoomId == roomId &&
            b.CheckIn < checkOut &&
            checkIn < b.CheckOut);
    }

    public bool TryCreateBooking(Booking booking)
    {
        if (booking.CheckIn.Date >= booking.CheckOut.Date)
        {
            throw new ArgumentException(
                "Check-out must be later than check-in.");
        }

        // The transaction coordinates the availability check and insertion so concurrent booking requests cannot both pass the conflict check.
        using var transaction = _context.Database.BeginTransaction(
            System.Data.IsolationLevel.Serializable);

        bool hasConflict = _context.Bookings.Any(b =>
            b.RoomId == booking.RoomId &&
            b.CheckIn < booking.CheckOut &&
            booking.CheckIn < b.CheckOut);

        if (hasConflict)
        {
            return false;
        }

        _context.Bookings.Add(booking);
        _context.SaveChanges();

        transaction.Commit();
        return true;
    }

    public Booking? GetBookingById(int bookingId)
    // Related guest and room information is needed when displaying and managing an existing reservation.
    {
        return _context.Bookings
            .Include(b => b.Guest)
            .Include(b => b.Room)
            .FirstOrDefault(b => b.Id == bookingId);   // LINQ FirstOrDefault() is to retrieve a booking by its ID.
    }

    public List<Room> GetAvailableRooms(
        DateTime checkIn,
        DateTime checkOut)
    // Exclude rooms with an overlapping reservation so that only rooms available for the entire period are returned.
    {
        return _context.Rooms
            .Include(r => r.RoomType)
            .Where(r => !_context.Bookings.Any(b =>
                b.RoomId == r.Id &&
                b.CheckIn < checkOut &&
                checkIn < b.CheckOut))
            .OrderBy(r => r.RoomNumber)
            .ToList();
    }

    public List<Treatment> GetAllTreatments()
    {
        return _context.Treatments
            .OrderBy(t => t.Id)
            .ToList();
    }

    public Treatment? GetTreatmentById(int treatmentId)
    {
        return _context.Treatments
            .FirstOrDefault(t => t.Id == treatmentId);
    }

    public void AddSpaBooking(SpaBooking spaBooking)
    {
        _context.SpaBookings.Add(spaBooking);
        _context.SaveChanges();
    }

    public Guest? GetGuestById(int guestId)
    {
        return _context.Guests
            .FirstOrDefault(g => g.Id == guestId);
    }


}
