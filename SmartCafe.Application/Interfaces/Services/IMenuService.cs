public interface IMenuService
{
    Task<List<MenuItemDto>> GetAllAsync();

    Task<MenuItemDto?> GetByIdAsync(Guid id);

    Task CreateAsync(CreateMenuItemDto dto);

    Task UpdateAsync(UpdateMenuItemDto dto);

    Task DeleteAsync(Guid id);
}
