namespace VehicleSystem.Application.DTOs
{
    public class FinancialReportDto
    {
        public string ReportType { get; set; } = string.Empty;

        public decimal TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalDiscountsGiven { get; set; }
        public decimal PendingCreditAmount { get; set; }

        public int TotalInvoices { get; set; }

        public List<TopSellingPartDto> TopSellingParts { get; set; } = new();
    }

    public class TopSellingPartDto
    {
        public int PartId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public int TotalQuantitySold { get; set; }
        public decimal TotalSalesAmount { get; set; }
    }
}