using Microsoft.EntityFrameworkCore;

public class TableRepository : ITableRepository
{
    private readonly AppDbContext _context;

    public TableRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CafeTable>> GetAllAsync()
    {
        return await _context.Tables.ToListAsync();
    }

    public async Task<CafeTable?> GetByIdAsync(Guid id)
    {
        return await _context.Tables.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(CafeTable table)
    {
        await _context.Tables.AddAsync(table);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CafeTable table)
    {
        _context.Tables.Update(table);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
            return;

        _context.Tables.Remove(entity);

        await _context.SaveChangesAsync();
    }
}
