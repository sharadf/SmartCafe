public interface IOrderService
{
    Task<List<OrderDto>> GetAllAsync();

    Task<OrderDto?> GetByIdAsync(Guid id);

    Task<List<OrderDto>> GetCustomerOrdersAsync(Guid customerId);

    Task CreateAsync(Guid customerId, CreateOrderDto dto);

    Task ChangeStatusAsync(Guid orderId, OrderStatus status);

    Task DeleteAsync(Guid id);
}
