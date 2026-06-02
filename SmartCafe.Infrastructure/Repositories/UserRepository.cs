using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly UserManager<AppUser> _userManager;

    public UserRepository(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<AppUser>> GetAllAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<AppUser?> GetByIdAsync(Guid id)
    {
        return await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<AppUser?> GetByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }
}
