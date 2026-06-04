public class CreateReservationDto
{
    public Guid TableId { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }
}
