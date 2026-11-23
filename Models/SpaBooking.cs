// // SpaBooking.cs - Represents a guest's spa reservation.
public class SpaBooking
{
    public int Id { get; set; }       // PK for each spa booking.

    public int GuestId { get; set; }  // FK linking the booking to a guest

    public Guest Guest { get; set; } = null!;

    public int TreatmentId { get; set; }    // FK linking to the selected treatment.

    public Treatment Treatment { get; set; } = null!;

    // Stores the appointment date and time for the spa treatment.
    public DateTime Date { get; set; }
}

