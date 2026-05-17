using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleSystem.Application.DTOs;
using VehicleSystem.Infrastructure.Data;

namespace VehicleSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotificationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStockNotifications()
        {
            var notifications = await _context.Parts
                .Where(p => p.StockQuantity < 10)
                .Select(p => new LowStockNotificationDto
                {
                    PartId = p.Id,
                    PartName = p.Name,
                    StockQuantity = p.StockQuantity,
                    Message = $"Low Stock Alert: {p.Name} has only {p.StockQuantity} units left."
                })
                .ToListAsync();

            return Ok(notifications);
        }
    }
}