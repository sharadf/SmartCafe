public class ReservationDto
{
    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public int TableNumber { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public ReservationStatus Status { get; set; }
}
