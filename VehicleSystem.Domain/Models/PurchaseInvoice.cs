namespace VehicleSystem.Domain.Models
{
    public class PurchaseInvoice
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public int AdminId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }

        // Navigation
        public Vendor Vendor { get; set; } = null!;
        public ICollection<PurchaseInvoiceItem> Items { get; set; } = new List<PurchaseInvoiceItem>();
    }
}