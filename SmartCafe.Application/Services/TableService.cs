public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;

    public TableService(ITableRepository tableRepository)
    {
        _tableRepository = tableRepository;
    }

    public async Task<List<TableDto>> GetAllAsync()
    {
        var tables = await _tableRepository.GetAllAsync();

        return tables
            .Select(x => new TableDto
            {
                Id = x.Id,
                Number = x.Number,
                Capacity = x.Capacity,
                Status = x.Status,
            })
            .ToList();
    }

    public async Task<TableDto?> GetByIdAsync(Guid id)
    {
        var table = await _tableRepository.GetByIdAsync(id);

        if (table == null)
            return null;

        return new TableDto
        {
            Id = table.Id,
            Number = table.Number,
            Capacity = table.Capacity,
            Status = table.Status,
        };
    }

    public async Task CreateAsync(CreateTableDto dto)
    {
        var table = new CafeTable
        {
            Id = Guid.NewGuid(),
            Number = dto.Number,
            Capacity = dto.Capacity,
            Status = TableStatus.Empty,
        };

        await _tableRepository.AddAsync(table);
    }

    public async Task UpdateStatusAsync(Guid tableId, TableStatus status)
    {
        var table = await _tableRepository.GetByIdAsync(tableId);

        if (table == null)
            throw new Exception("Table not found");

        table.Status = status;

        await _tableRepository.UpdateAsync(table);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _tableRepository.DeleteAsync(id);
    }
}
