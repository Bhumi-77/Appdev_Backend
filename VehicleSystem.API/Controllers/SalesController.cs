using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle_System.DTOs;
using Vehicle_System.Models;
using VehicleSystem.Infrastructure.Data;

namespace Vehicle_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalesController(AppDbContext context)
        {
            _context = context;
        }

        // Feature 7: Staff can sell vehicle parts and create sales invoices
        // Feature 16: Loyalty Program: Customers get 10% discount if they spend more than 5000 in a single purchase
        [HttpPost("sell")]
        public async Task<IActionResult> Sell(SellDto sellDto)
        {
            var invoice = new Invoice
            {
                CustomerId = sellDto.CustomerId,
                InvoiceItems = new List<InvoiceItem>()
            };

            decimal totalBeforeDiscount = 0;

            foreach (var item in sellDto.Items)
            {
                var part = await _context.Parts.FindAsync(item.PartId);
                if (part == null) return BadRequest($"Part with ID {item.PartId} not found.");
                if (part.Stock < item.Quantity) return BadRequest($"Insufficient stock for part: {part.Id}");

                var invoiceItem = new InvoiceItem
                {
                    PartId = item.PartId,
                    Quantity = item.Quantity,
                    Price = part.Price
                };

                totalBeforeDiscount += invoiceItem.Quantity * invoiceItem.Price;
                invoice.InvoiceItems.Add(invoiceItem);

                // Update stock
                part.Stock -= item.Quantity;
            }

            // Feature 16: Loyalty Program - 10% discount if spend more than 5000
            if (totalBeforeDiscount > 5000)
            {
                invoice.TotalAmount = totalBeforeDiscount * 0.9m;
            }
            else
            {
                invoice.TotalAmount = totalBeforeDiscount;
            }

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return Ok(invoice);
        }
    }
}
