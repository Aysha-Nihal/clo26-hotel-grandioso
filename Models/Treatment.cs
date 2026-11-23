// Treatment.cs - Represents a spa treatment offered by the hotel.
public class Treatment
{
    public int Id { get; set; }   // PK for each treatment.

    public string Name { get; set; } = string.Empty;

    public int DurationMinutes { get; set; }  // Duration of the treatment in minutes.

    public decimal Price { get; set; }   // Price of the treatment.

    // A treatment can be included in multiple spa bookings.
    public List<SpaBooking> SpaBookings { get; set; } = new();
}
