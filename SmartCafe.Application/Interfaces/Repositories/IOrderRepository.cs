public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();

    Task<Order?> GetByIdAsync(Guid id);

    Task<List<Order>> GetByCustomerIdAsync(Guid customerId);

    Task AddAsync(Order order);

    Task UpdateAsync(Order order);

    Task DeleteAsync(Guid id);
}
