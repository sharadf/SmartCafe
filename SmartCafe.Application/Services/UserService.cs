using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    private readonly IJwtService _jwtService;

    private readonly IMapper _mapper;

    public UserService(UserManager<AppUser> userManager, IJwtService jwtService, IMapper mapper)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new Exception("User already exists.");

        var user = _mapper.Map<AppUser>(dto);

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(x => x.Description)));

        await _userManager.AddToRoleAsync(user, "Customer");
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            throw new Exception("Invalid credentials");

        var isValid = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (!isValid)
            throw new Exception("Invalid credentials");

        var token = _jwtService.GenerateToken(user);

        return new AuthResponseDto { Token = token, ExpiresAt = DateTime.UtcNow.AddDays(7) };
    }

    public async Task<List<AppUser>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<AppUser?> GetUserByIdAsync(Guid id)
    {
        return await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
            throw new Exception("User not found");

        await _userManager.DeleteAsync(user);
    }
}
