using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class AppDbSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

        var db = serviceProvider.GetRequiredService<AppDbContext>();

        await SeedRoles(roleManager);
        await SeedUsers(userManager);
        await SeedTables(db);
        await SeedMenu(db);
    }

    private static async Task SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
    {
        string[] roles = { "Admin", "Waiter", "Customer" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid> { Name = role });
            }
        }
    }

    private static async Task SeedUsers(UserManager<AppUser> userManager)
    {
        if (userManager.Users.Any())
            return;

        var users = new[]
        {
            new
            {
                Name = "System Admin",
                Email = "admin@cafe.az",
                Role = "Admin",
            },
            new
            {
                Name = "Waiter One",
                Email = "waiter1@cafe.az",
                Role = "Waiter",
            },
            new
            {
                Name = "Waiter Two",
                Email = "waiter2@cafe.az",
                Role = "Waiter",
            },
            new
            {
                Name = "Waiter Three",
                Email = "waiter3@cafe.az",
                Role = "Waiter",
            },
            new
            {
                Name = "Customer One",
                Email = "customer1@gmail.com",
                Role = "Customer",
            },
            new
            {
                Name = "Customer Two",
                Email = "customer2@gmail.com",
                Role = "Customer",
            },
        };

        foreach (var item in users)
        {
            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                FullName = item.Name,
                Email = item.Email,
                UserName = item.Email,
            };

            await userManager.CreateAsync(user, "Password123!");

            await userManager.AddToRoleAsync(user, item.Role);
        }
    }

    private static async Task SeedTables(AppDbContext db)
    {
        if (db.Tables.Any())
            return;

        db.Tables.AddRange(
            new CafeTable
            {
                Id = Guid.NewGuid(),
                Number = 1,
                Capacity = 2,
                Status = TableStatus.Empty,
            },
            new CafeTable
            {
                Id = Guid.NewGuid(),
                Number = 2,
                Capacity = 2,
                Status = TableStatus.Empty,
            },
            new CafeTable
            {
                Id = Guid.NewGuid(),
                Number = 3,
                Capacity = 4,
                Status = TableStatus.Empty,
            },
            new CafeTable
            {
                Id = Guid.NewGuid(),
                Number = 4,
                Capacity = 4,
                Status = TableStatus.Empty,
            },
            new CafeTable
            {
                Id = Guid.NewGuid(),
                Number = 5,
                Capacity = 6,
                Status = TableStatus.Empty,
            }
        );

        await db.SaveChangesAsync();
    }

    private static async Task SeedMenu(AppDbContext db)
    {
        if (db.MenuItems.Any())
            return;

        db.MenuItems.AddRange(
            new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = "Cappuccino",
                Description = "Italian coffee",
                Price = 4.5m,
                Category = "Coffee",
                PhotoUrl = "",
            },
            new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = "Latte",
                Description = "Milk coffee",
                Price = 5m,
                Category = "Coffee",
                PhotoUrl = "",
            },
            new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = "Espresso",
                Description = "Strong coffee",
                Price = 3m,
                Category = "Coffee",
                PhotoUrl = "",
            },
            new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = "Pizza Margarita",
                Description = "Classic pizza",
                Price = 12m,
                Category = "Food",
                PhotoUrl = "",
            },
            new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = "Cheeseburger",
                Description = "Beef burger",
                Price = 9m,
                Category = "Food",
                PhotoUrl = "",
            },
            new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = "Caesar Salad",
                Description = "Fresh salad",
                Price = 8m,
                Category = "Food",
                PhotoUrl = "",
            }
        );

        await db.SaveChangesAsync();
    }
}
