using Microsoft.EntityFrameworkCore;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _context;

    public ReservationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Reservation>> GetAllAsync()
    {
        return await _context
            .Reservations.Include(x => x.Customer)
            .Include(x => x.Table)
            .ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync(Guid id)
    {
        return await _context
            .Reservations.Include(x => x.Customer)
            .Include(x => x.Table)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Reservation>> GetByTableIdAsync(Guid tableId)
    {
        return await _context.Reservations.Where(x => x.TableId == tableId).ToListAsync();
    }

    public async Task AddAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Reservation reservation)
    {
        _context.Reservations.Update(reservation);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
            return;

        _context.Reservations.Remove(entity);

        await _context.SaveChangesAsync();
    }
}
