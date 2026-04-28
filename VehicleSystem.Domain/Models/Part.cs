namespace VehicleSystem.Domain.Models
{
    public class Part
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal SellingPrice { get; set; }
        public decimal CostPrice { get; set; }
        public int StockQuantity { get; set; }
        public int LowStockThreshold { get; set; } = 10;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation to vendors
        public Vendor Vendor { get; set; } = null!;
    }
}