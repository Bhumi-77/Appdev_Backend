using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleSystem.Application.DTOs;
using VehicleSystem.Application.Interfaces.Services;
using VehicleSystem.Infrastructure.Data;

namespace VehicleSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditRemindersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public CreditRemindersController(
            AppDbContext context,
            IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingCreditReminders()
        {
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);

            var reminders = await _context.Invoices
                .Include(i => i.Customer)
                .Where(i =>
                    (i.PaymentStatus == "Credit" || i.PaymentStatus == "Unpaid") &&
                    i.InvoiceDate <= oneMonthAgo
                )
                .Select(i => new PendingCreditReminderDto
                {
                    InvoiceId = i.Id,
                    CustomerId = i.CustomerId,
                    CustomerName = i.Customer != null ? i.Customer.Name : "Unknown Customer",
                    CustomerEmail = i.Customer != null ? i.Customer.Email : "No Email",
                    AmountDue = i.FinalAmount,
                    InvoiceDate = i.InvoiceDate,
                    PaymentStatus = i.PaymentStatus,
                    Message = $"Payment Reminder: Invoice #{i.Id} has been unpaid for more than 1 month. Pending amount: Rs. {i.FinalAmount}."
                })
                .ToListAsync();

            return Ok(reminders);
        }

        [HttpPost("send/{invoiceId}")]
        public async Task<IActionResult> SendReminderEmail(int invoiceId)
        {
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(i => i.Id == invoiceId);

            if (invoice == null)
                return NotFound("Invoice not found.");

            if (invoice.PaymentStatus != "Credit" && invoice.PaymentStatus != "Unpaid")
                return BadRequest("This invoice has no pending credit.");

            if (invoice.InvoiceDate > oneMonthAgo)
                return BadRequest("Reminder can only be sent for credits unpaid for more than 1 month.");

            if (invoice.Customer == null || string.IsNullOrWhiteSpace(invoice.Customer.Email))
                return BadRequest("Customer email is not available.");

            var subject = $"Payment Reminder for Invoice #{invoice.Id}";

            var body = $@"
                <h2>Payment Reminder</h2>
                <p>Dear {invoice.Customer.Name},</p>
                <p>This is a reminder that your payment has been pending for more than one month.</p>

                <table border='1' cellpadding='8' cellspacing='0'>
                    <tr>
                        <td><strong>Invoice ID</strong></td>
                        <td>#{invoice.Id}</td>
                    </tr>
                    <tr>
                        <td><strong>Amount Due</strong></td>
                        <td>Rs. {invoice.FinalAmount}</td>
                    </tr>
                    <tr>
                        <td><strong>Payment Status</strong></td>
                        <td>{invoice.PaymentStatus}</td>
                    </tr>
                    <tr>
                        <td><strong>Invoice Date</strong></td>
                        <td>{invoice.InvoiceDate:yyyy-MM-dd}</td>
                    </tr>
                </table>

                <p>Please clear your pending payment as soon as possible.</p>
                <p>Thank you,<br/>AutoPart Admin Team</p>
            ";

            await _emailService.SendEmailAsync(
                invoice.Customer.Email,
                subject,
                body
            );

            return Ok(new
            {
                success = true,
                message = $"Reminder email sent to {invoice.Customer.Email} for Invoice #{invoice.Id}.",
                invoiceId = invoice.Id,
                amountDue = invoice.FinalAmount
            });
        }
    }
}