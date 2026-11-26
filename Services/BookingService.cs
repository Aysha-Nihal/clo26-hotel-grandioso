
public class BookingService
{
    private readonly IHotelRepository _repository;          // Constructor injection

    // Using the interface keeps business logic independent of EF Core.
    public BookingService(IHotelRepository repository)
    {
        _repository = repository;
    }

    // A booking requires at least one night.
    public bool AreDatesValid(DateTime checkIn, DateTime checkOut)
    {
        return checkIn.Date < checkOut.Date;                 // Date validation
    }

    // Calculate the price based on the number of nights.
    public decimal CalculateTotalPrice(
        Room room,
        DateTime checkIn,
        DateTime checkOut)
    {
        if (!AreDatesValid(checkIn, checkOut))
        {
            throw new ArgumentException(
                "Check-out must be later than check-in.");
        }

        int nights = (checkOut.Date - checkIn.Date).Days;    // Price calculation

        return nights * room.PricePerNight;
    }


    public bool IsRoomAvailable(
        int roomId,
        DateTime checkIn,
        DateTime checkOut)
    {
        if (!AreDatesValid(checkIn, checkOut))
        {
            return false;
        }

        // A room is available when no existing booking overlaps the requested dates.
        return !_repository.HasOverlappingBooking(
            roomId,
            checkIn.Date,
            checkOut.Date);
    }



    public bool CreateBooking(
        int roomNumber,
        int guestId,
        DateTime checkIn,
        DateTime checkOut)
    {
        if (!AreDatesValid(checkIn, checkOut))
        {
            throw new ArgumentException(
                "Invalid booking dates.");
        }

        Room? room = _repository.GetRoomByNumber(roomNumber);

        if (room == null)
        {
            throw new ArgumentException(
                "The requested room does not exist.");
        }

        Booking booking = new Booking
        {
            RoomId = room.Id,
            GuestId = guestId,
            CheckIn = checkIn.Date,
            CheckOut = checkOut.Date
        };

        return _repository.TryCreateBooking(booking);
    }


    public bool CancelBooking(int bookingId)
    {
        // Avoid attempting to delete a booking that does not exist.
        Booking? booking = _repository.GetBookingById(bookingId);

        if (booking == null)
        {
            return false;
        }

        _repository.RemoveBooking(booking);

        return true;
    }



    public List<Room> GetAvailableRooms(
        DateTime checkIn,
        DateTime checkOut)
    {
        if (!AreDatesValid(checkIn, checkOut))
        {
            throw new ArgumentException(
                "Check-out must be later than check-in.");
        }

        return _repository.GetAvailableRooms(
            checkIn.Date,
            checkOut.Date);
    }




}


// HasOverlappingBooking() == true → room unavailable.
// HasOverlappingBooking() == false → room available.