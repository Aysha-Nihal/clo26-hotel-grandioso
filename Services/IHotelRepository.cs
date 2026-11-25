// IHotelRepository.cs :— Defines what database operations are available.
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
    void AddBooking(Booking booking);

    // Cancel an existing room booking.
    void RemoveBooking(Booking booking);
}
