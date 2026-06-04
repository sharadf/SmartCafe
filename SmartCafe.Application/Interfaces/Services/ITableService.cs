public interface ITableService
{
    Task<List<TableDto>> GetAllAsync();

    Task<TableDto?> GetByIdAsync(Guid id);

    Task CreateAsync(CreateTableDto dto);

    Task UpdateStatusAsync(Guid tableId, TableStatus status);

    Task DeleteAsync(Guid id);
}
