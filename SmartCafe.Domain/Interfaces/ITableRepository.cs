public interface ITableRepository
{
    Task<List<CafeTable>> GetAllAsync();

    Task<CafeTable?> GetByIdAsync(Guid id);

    Task AddAsync(CafeTable table);

    Task UpdateAsync(CafeTable table);

    Task DeleteAsync(Guid id);
}
