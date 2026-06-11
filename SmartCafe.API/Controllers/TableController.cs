using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TableController : ControllerBase
{
    private readonly ITableService _tableService;

    public TableController(ITableService tableService)
    {
        _tableService = tableService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _tableService.GetAllAsync());
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateTableDto dto)
    {
        await _tableService.CreateAsync(dto);

        return Ok();
    }

    [Authorize(Roles = "Admin,Waiter")]
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> ChangeStatus(Guid id, TableStatus status)
    {
        await _tableService.UpdateStatusAsync(id, status);

        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _tableService.DeleteAsync(id);

        return Ok();
    }
}
