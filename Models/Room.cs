// Room.cs :— defines individual hotel rooms.
public class Room
{
    public int Id { get; set; }           // PK for the room
    public int RoomNumber { get; set; }   // Hotel room number
    public int Floor { get; set; }        // Floor number
    public int SizeM2 { get; set; }      //  Room size
    public decimal PricePerNight { get; set; }   // Nightly price
    public int RoomTypeId { get; set; }          // Foreign key to RoomType

    public RoomType RoomType { get; set; } = null!;

    // A room can be booked multiple times on different dates.
    public List<Booking> Bookings { get; set; } = new();
    // Prevent invalid room prices when changing the price.
    public void ChangePrice(decimal newPrice)    // Method
    {
        if (newPrice <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(newPrice),
                "Price must be greater than zero.");
        }

        PricePerNight = newPrice;
    }
}