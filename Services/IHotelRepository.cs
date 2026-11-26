// IHotelRepository.cs :— Defines what database operations are available.
//                        Defines the repository interface.

using Microsoft.Data.Sqlite;
public interface IHotelRepository
{
    // Retrieve all rooms from the database.
    List<Room> GetAllRooms();

    // Retrieve all existing room bookings.
    List<Booking> GetAllBookings();

    // Find a room by its hotel room number.
    Room? GetRoomByNumber(int roomNumber);

    // Find a guest using their name.
    Guest? GetGuestByName(string name);

    // Store a new guest.
    void AddGuest(Guest guest);

    // Store a new room booking.
    //void AddBooking(Booking booking);

    // Cancel an existing room booking.
    void RemoveBooking(Booking booking);


    // Check whether a room has an overlapping booking.
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
