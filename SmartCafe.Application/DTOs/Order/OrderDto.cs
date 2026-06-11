public class OrderDto
{
    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public DateTime CreatedAt { get; set; }

    public OrderStatus Status { get; set; }

    public decimal TotalPrice { get; set; }

    public List<CreateOrderItemDto> Items { get; set; } = new();
}
