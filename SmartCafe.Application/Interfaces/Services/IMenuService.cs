public interface IMenuService
{
    Task<List<MenuItemDto>> GetMenuItemsAsync();

    Task<MenuItemDto?> GetByIdAsync(Guid id);

    Task CreateAsync(CreateMenuItemDto dto);

    Task UpdateAsync(UpdateMenuItemDto dto);

    Task DeleteAsync(Guid id);
}
