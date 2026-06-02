public interface IMenuRepository
{
    Task<List<MenuItem>> GetAllAsync();

    Task<MenuItem?> GetByIdAsync(Guid id);

    Task AddAsync(MenuItem menuItem);

    Task UpdateAsync(MenuItem menuItem);

    Task DeleteAsync(Guid id);
}
