public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<List<MenuItemDto>> GetAllAsync()
    {
        var items = await _menuRepository.GetAllAsync();

        return items
            .Select(x => new MenuItemDto
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Category = x.Category,
            })
            .ToList();
    }

    public async Task<MenuItemDto?> GetByIdAsync(Guid id)
    {
        var item = await _menuRepository.GetByIdAsync(id);

        if (item == null)
            return null;

        return new MenuItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Price = item.Price,
            Category = item.Category,
        };
    }

    public async Task CreateAsync(CreateMenuItemDto dto)
    {
        var menuItem = new MenuItem
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Category = dto.Category,
            PhotoUrl = dto.PhotoUrl,
        };
        await _menuRepository.AddAsync(menuItem);
    }

    public async Task UpdateAsync(UpdateMenuItemDto dto)
    {
        var item = await _menuRepository.GetByIdAsync(dto.Id);

        if (item == null)
            throw new Exception("Menu item not found");

        item.Name = dto.Name;
        item.Description = dto.Description;
        item.Price = dto.Price;
        item.Category = dto.Category;
        item.PhotoUrl = dto.PhotoUrl;

        await _menuRepository.UpdateAsync(item);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _menuRepository.DeleteAsync(id);
    }
}
