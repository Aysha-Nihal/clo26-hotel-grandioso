public class SpaService
{
    private readonly IHotelRepository _repository;

    public SpaService(IHotelRepository repository)
    {
        _repository = repository;
    }

    // Return all spa treatments with their prices and durations.
    public List<Treatment> GetTreatments()
    {
        return _repository.GetAllTreatments();
    }

    //// Create a spa booking for an existing guest.
    //public bool BookTreatment(
    //    int guestId,
    //    int treatmentId,
    //    DateTime date)
    //{
    //    Treatment? treatment =
    //        _repository.GetTreatmentById(treatmentId);

    //    if (treatment == null)
    //    {
    //        return false;
    //    }

    //    SpaBooking booking = new SpaBooking
    //    {
    //        GuestId = guestId,
    //        TreatmentId = treatmentId,
    //        Date = date
    //    };

    //    _repository.AddSpaBooking(booking);

    //    return true;
    //}



    public bool BookTreatment(
        int guestId,
        int treatmentId,
        DateTime date)
    {
        // Verify that the guest exists.
        Guest? guest = _repository.GetGuestById(guestId);

        if (guest == null)
        {
            return false;
        }

        // Verify that the selected treatment exists.
        Treatment? treatment =
            _repository.GetTreatmentById(treatmentId);

        if (treatment == null)
        {
            return false;
        }

        // Create and save the spa reservation.
        SpaBooking booking = new SpaBooking
        {
            GuestId = guestId,
            TreatmentId = treatmentId,
            Date = date
        };

        _repository.AddSpaBooking(booking);

        return true;
    }

}
