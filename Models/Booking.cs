// Booking.cs :— connects guests to rooms for specific dates.
public class Booking
{
    public int Id { get; set; }   // PK for each booking
    public int GuestId { get; set; }   //FK to Guest
    public Guest Guest { get; set; } = null!;  // Navigation property to the Guest entity
    public int RoomId { get; set; }     // FK to Room
    public Room Room { get; set; } = null!;    // Navigation property to the Room entity
    public DateTime CheckIn { get; set; }    // Date when the guest checks in
    public DateTime CheckOut { get; set; }
}
