public class CafeTable
{
    public Guid Id { get; set; }

    public int Number { get; set; }

    public int Capacity { get; set; }

    public TableStatus Status { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
