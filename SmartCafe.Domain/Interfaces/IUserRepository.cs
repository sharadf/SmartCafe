public interface IUserRepository
{
    Task<List<AppUser>> GetAllAsync();

    Task<AppUser?> GetByIdAsync(Guid id);

    Task<AppUser?> GetByEmailAsync(string email);
}
