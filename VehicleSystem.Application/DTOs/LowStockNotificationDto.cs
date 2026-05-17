namespace VehicleSystem.Application.DTOs
{
    public class LowStockNotificationDto
    {
        public int PartId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}