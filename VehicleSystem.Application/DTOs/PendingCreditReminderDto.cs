namespace VehicleSystem.Application.DTOs
{
    public class PendingCreditReminderDto
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;

        public decimal AmountDue { get; set; }
        public DateTime InvoiceDate { get; set; }

        public string PaymentStatus { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}