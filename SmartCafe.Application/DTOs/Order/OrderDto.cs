public class OrderDto
{
    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public List<CreateOrderItemDto> Items { get; set; } = new();
}
