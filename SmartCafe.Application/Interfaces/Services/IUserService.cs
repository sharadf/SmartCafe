public interface IUserService
{
    Task<AuthResponseDto> LoginAsync(LoginDto dto);

    Task RegisterAsync(RegisterDto dto);

    Task<List<AppUser>> GetAllUsersAsync();

    Task<AppUser?> GetUserByIdAsync(Guid id);

    Task DeleteUserAsync(Guid id);
}
