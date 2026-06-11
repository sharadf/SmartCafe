using AutoMapper;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;

    private readonly IMapper _mapper;

    public TableService(ITableRepository tableRepository, IMapper mapper)
    {
        _tableRepository = tableRepository;
        _mapper = mapper;
    }

    public async Task<List<TableDto>> GetAllAsync()
    {
        var tables = await _tableRepository.GetAllAsync();

        return _mapper.Map<List<TableDto>>(tables);
    }

    public async Task<TableDto?> GetByIdAsync(Guid id)
    {
        var table = await _tableRepository.GetByIdAsync(id);

        if (table == null)
            return null;

        return _mapper.Map<TableDto>(table);
    }

    public async Task CreateAsync(CreateTableDto dto)
    {
        var table = _mapper.Map<CafeTable>(dto);

        table.Id = Guid.NewGuid();

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
