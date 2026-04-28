namespace VehicleSystem.Application.DTOs
{
    public class FinancialReportDto
    {
        public string ReportType { get; set; } = string.Empty;
        public decimal TotalSales { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalCredit { get; set; }
        public int TotalInvoices { get; set; }
    }
}