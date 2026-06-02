using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser<Guid>
{
    public string FullName { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
