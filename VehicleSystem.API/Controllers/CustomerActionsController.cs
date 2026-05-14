using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle_System.Models;
using VehicleSystem.Infrastructure.Data;

namespace Vehicle_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerActionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerActionsController(AppDbContext context)
        {
            _context = context;
        }

        // Feature 13: Customers can book appointments
        [HttpPost("book-appointment")]
        public async Task<IActionResult> BookAppointment(Appointment appointment)
        {
            appointment.Status = "Pending";
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return Ok(appointment);
        }

        // Feature 13: Customers can request unavailable parts
        [HttpPost("request-part")]
        public async Task<IActionResult> RequestPart(PartRequest request)
        {
            request.Status = "Requested";
            _context.PartRequests.Add(request);
            await _context.SaveChangesAsync();
            return Ok(request);
        }

        // Feature 13: Customers can review services
        [HttpPost("review-service")]
        public async Task<IActionResult> ReviewService(ServiceReview review)
        {
            review.ReviewDate = DateTime.Now;
            _context.ServiceReviews.Add(review);
            await _context.SaveChangesAsync();
            return Ok(review);
        }

        // Feature 12: Customers can manage vehicle details
        [HttpPost("add-vehicle")]
        public async Task<IActionResult> AddVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return Ok(vehicle);
        }

        [HttpPut("update-vehicle/{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, Vehicle updated)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return NotFound();

            vehicle.VehicleNumber = updated.VehicleNumber;
            await _context.SaveChangesAsync();
            return Ok(vehicle);
        }

        // Feature 14: Customers can view their purchase/service history
        [HttpGet("history/{customerId}")]
        public async Task<IActionResult> GetHistory(int customerId)
        {
            var invoices = await _context.Invoices
                .Include(i => i.InvoiceItems)
                .Where(i => i.CustomerId == customerId)
                .ToListAsync();

            var appointments = await _context.Appointments
                .Where(a => a.CustomerId == customerId)
                .ToListAsync();

            return Ok(new
            {
                Invoices = invoices,
                Appointments = appointments
            });
        }
    }
}
