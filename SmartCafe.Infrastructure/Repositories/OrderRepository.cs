using Microsoft.EntityFrameworkCore;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context
            .Orders.Include(x => x.Customer)
            .Include(x => x.Table)
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.MenuItem)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context
            .Orders.Include(x => x.Customer)
            .Include(x => x.Table)
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.MenuItem)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Order>> GetByCustomerIdAsync(Guid customerId)
    {
        return await _context.Orders.Where(x => x.CustomerId == customerId).ToListAsync();
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
            return;

        _context.Orders.Remove(entity);

        await _context.SaveChangesAsync();
    }
}
