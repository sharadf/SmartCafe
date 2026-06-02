public class Order
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public AppUser Customer { get; set; } = null!;

    public Guid TableId { get; set; }

    public CafeTable Table { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public OrderStatus Status { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
