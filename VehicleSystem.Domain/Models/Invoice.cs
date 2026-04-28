namespace VehicleSystem.Domain.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int? StaffId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal FinalAmount { get; set; }

        public string PaymentStatus { get; set; } = "Unpaid";

        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Customer? Customer { get; set; }

        public User? Staff { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; } = new();
    }
}