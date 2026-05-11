namespace VehicleSystem.Application.DTOs
{
    // Invoice via the clients
    public class PurchaseInvoiceItemRequestDto
    {
        public int PartId { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
    }

    // CREATE an invoice
    public class PurchaseInvoiceRequestDto
    {
        public int VendorId { get; set; }
        public int AdminId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }
        public List<PurchaseInvoiceItemRequestDto> Items { get; set; } = new();
    }

    // Item to send back to the clients
    public class PurchaseInvoiceItemResponseDto
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    // Invoice sent back to the client
    public class PurchaseInvoiceResponseDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public int VendorId { get; set; }
        public string VendorName { get; set; } = string.Empty;
        public int AdminId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? Notes { get; set; }
        public List<PurchaseInvoiceItemResponseDto> Items { get; set; } = new();
    }
}