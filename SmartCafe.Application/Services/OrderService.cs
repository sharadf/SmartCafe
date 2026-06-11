using AutoMapper;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    private readonly ITableRepository _tableRepository;

    private readonly IMenuRepository _menuRepository;

    private readonly IMapper _mapper;

    public OrderService(
        IOrderRepository orderRepository,
        ITableRepository tableRepository,
        IMenuRepository menuRepository,
        IMapper mapper
    )
    {
        _orderRepository = orderRepository;
        _tableRepository = tableRepository;
        _menuRepository = menuRepository;
        _mapper = mapper;
    }

    public async Task<List<OrderDto>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllAsync();

        return _mapper.Map<List<OrderDto>>(orders);
    }

    public async Task<OrderDto?> GetByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order == null)
            return null;

        return _mapper.Map<OrderDto>(order);
    }

    public async Task<List<OrderDto>> GetCustomerOrdersAsync(Guid customerId)
    {
        var orders = await _orderRepository.GetCustomerOrdersAsync(customerId);

        return _mapper.Map<List<OrderDto>>(orders);
    }

    public async Task CreateAsync(Guid customerId, CreateOrderDto dto)
    {
        var table = await _tableRepository.GetByIdAsync(dto.TableId);
        if (table == null)
            throw new Exception("Table not found");

        var order = _mapper.Map<Order>(table);

        order.Id = Guid.NewGuid();
        order.CustomerId = customerId;
        order.CreatedAt = DateTime.UtcNow;
        order.Status = OrderStatus.Pending;

        foreach (var orderItem in order.OrderItems)
        {
            var menuItem = await _menuRepository.GetByIdAsync(orderItem.MenuItemId);
            if (menuItem == null)
                throw new Exception("Menu item not found");

            orderItem.Id = Guid.NewGuid();
            orderItem.Price = menuItem.Price;
        }

        table.Status = TableStatus.Occupied;
        await _tableRepository.UpdateAsync(table);

        await _orderRepository.AddAsync(order);
    }

    public async Task ChangeStatusAsync(Guid orderId, OrderStatus status)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order == null)
            throw new Exception("Order not found");

        order.Status = status;

        await _orderRepository.UpdateAsync(order);

        if (status == OrderStatus.Paid)
        {
            var table = await _tableRepository.GetByIdAsync(order.TableId);

            if (table != null)
            {
                table.Status = TableStatus.Empty;

                await _tableRepository.UpdateAsync(table);
            }
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        await _orderRepository.DeleteAsync(id);
    }
}
