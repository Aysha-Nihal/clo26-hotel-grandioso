// IHotelRepository.cs :— Defines what database operations are available.
//                        Defines the repository interface.

using Microsoft.Data.Sqlite;
public interface IHotelRepository
{
    // Room information is retrieved through the repository so the
    // rest of the application does not depend directly on EF Core.
    List<Room> GetAllRooms();

    // Bookings include related guest and room information.
    List<Booking> GetAllBookings();

    // Find a room by its hotel room number.
    Room? GetRoomByNumber(int roomNumber);

    // Guest records are shared between room bookings and spa bookings.
    Guest? GetGuestByName(string name);

    // Store a new guest.
    void AddGuest(Guest guest);

    // Cancel an existing room booking.
    void RemoveBooking(Booking booking);

    // The overlap check supports availability searches.
    bool HasOverlappingBooking(
        int roomId,
        DateTime checkIn,
        DateTime checkOut);

    // Create a reservation only when the room is not already booked.
    bool TryCreateBooking(Booking booking);

    // Find a booking by its database ID.
    Booking? GetBookingById(int bookingId);

    // Find rooms without reservations overlapping the requested dates.
    List<Room> GetAvailableRooms(
        DateTime checkIn,
        DateTime checkOut);

    // Retrieve all available spa treatments.
    List<Treatment> GetAllTreatments();

    // Find a treatment by its ID.
    Treatment? GetTreatmentById(int treatmentId);

    // Save a spa reservation.
    void AddSpaBooking(SpaBooking spaBooking);

    Guest? GetGuestById(int guestId);

}
