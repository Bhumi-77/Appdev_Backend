namespace VehicleSystem.Domain.Models
{
    public class PurchaseInvoiceItem
    {
        public int Id { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public int PartId { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal LineTotal { get; set; }

        // Navigation
        public PurchaseInvoice PurchaseInvoice { get; set; } = null!;
        public Part Part { get; set; } = null!;
    }
}