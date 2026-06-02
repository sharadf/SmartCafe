public class Reservation
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public AppUser Customer { get; set; } = null!;

    public Guid TableId { get; set; }

    public CafeTable Table { get; set; } = null!;

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public ReservationStatus Status { get; set; }
}
