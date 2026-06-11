using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    private Guid UserId => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [Authorize(Roles = "Admin,Waiter")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _reservationService.GetAllAsync();

        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _reservationService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [Authorize(Roles = "Customer")]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyReservations()
    {
        var result = await _reservationService.GetCustomerReservationsAsync(UserId);

        return Ok(result);
    }

    [Authorize(Roles = "Customer")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateReservationDto dto)
    {
        await _reservationService.CreateAsync(UserId, dto);

        return Ok("Reservation created successfully");
    }

    [Authorize(Roles = "Customer,Admin")]
    [HttpPatch("cancel/{id:guid}")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        await _reservationService.CancelAsync(id);

        return Ok("Reservation cancelled");
    }
}
