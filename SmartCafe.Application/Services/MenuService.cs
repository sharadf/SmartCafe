using AutoMapper;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;
    private readonly IMapper _mapper;

    public MenuService(IMenuRepository menuRepository, IMapper mapper)
    {
        _menuRepository = menuRepository;
        _mapper = mapper;
    }

    public async Task<List<MenuItemDto>> GetAllAsync()
    {
        var items = await _menuRepository.GetAllAsync();

        return _mapper.Map<List<MenuItemDto>>(items);
    }

    public async Task<MenuItemDto?> GetByIdAsync(Guid id)
    {
        var item = await _menuRepository.GetByIdAsync(id);

        if (item == null)
            return null;

        return _mapper.Map<MenuItemDto>(item);
    }

    public async Task CreateAsync(CreateMenuItemDto dto)
    {
        var menuItem = _mapper.Map<MenuItem>(dto);

        menuItem.Id = Guid.NewGuid();

        await _menuRepository.AddAsync(menuItem);
    }

    public async Task UpdateAsync(UpdateMenuItemDto dto)
    {
        var item = await _menuRepository.GetByIdAsync(dto.Id);

        if (item == null)
            throw new Exception("Menu item not found");

        _mapper.Map(dto, item);

        await _menuRepository.UpdateAsync(item);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _menuRepository.DeleteAsync(id);
    }
}
