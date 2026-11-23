// Guest.cs :— defines hotel guests and their reservations.
public class Guest
{
    public int Id { get; set; }     // PK to each guest
    public string Name { get; set; } = string.Empty;   // // Guest's name used for reservations.

    // A guest can have multiple room reservations.
    public List<Booking> Bookings { get; set; } = new();

    // A guest can book multiple spa treatments.
    public List<SpaBooking> SpaBookings { get; set; } = new();
}
