using Microsoft.EntityFrameworkCore;
using VehicleSystem.Application.DTOs;
using VehicleSystem.Application.Interfaces.Repositories;
using VehicleSystem.Domain.Models;
using VehicleSystem.Infrastructure.Data;

namespace VehicleSystem.Infrastructure.Repositories
{
    public class FinancialReportRepository : IFinancialReportRepository
    {
        private readonly AppDbContext _context;

        public FinancialReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FinancialReportDto> GetDailyReportAsync(DateTime date)
        {
            var startDate = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
            var endDate = startDate.AddDays(1);

            return await BuildReport("Daily", startDate, endDate);
        }

        public async Task<FinancialReportDto> GetMonthlyReportAsync(int year, int month)
        {
            var startDate = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
            var endDate = startDate.AddMonths(1);

            return await BuildReport("Monthly", startDate, endDate);
        }

        public async Task<FinancialReportDto> GetYearlyReportAsync(int year)
        {
            var startDate = new DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var endDate = startDate.AddYears(1);

            return await BuildReport("Yearly", startDate, endDate);
        }

        private async Task<FinancialReportDto> BuildReport(string type, DateTime startDate, DateTime endDate)
        {
            var invoices = await _context.Invoices
                .Where(i => i.InvoiceDate >= startDate && i.InvoiceDate < endDate)
                .ToListAsync();

            var invoiceIds = invoices.Select(i => i.Id).ToList();

            var topSellingParts = await _context.InvoiceItems
                .Where(ii => invoiceIds.Contains(ii.InvoiceId))
                .GroupBy(ii => ii.PartId)
                .Select(g => new
                {
                    PartId = g.Key,
                    TotalQuantitySold = g.Sum(x => x.Quantity),
                    TotalSalesAmount = g.Sum(x => x.Quantity * x.Price)
                })
                .OrderByDescending(x => x.TotalQuantitySold)
                .Take(5)
                .ToListAsync();

            var partIds = topSellingParts.Select(x => x.PartId).ToList();

            var parts = await _context.Parts
                .Where(p => partIds.Contains(p.Id))
                .ToListAsync();

            return new FinancialReportDto
            {
                ReportType = type,

                TotalSales = invoices.Sum(i => i.FinalAmount),

                TotalRevenue = invoices
                    .Where(i => i.PaymentStatus == "Paid")
                    .Sum(i => i.FinalAmount),

                TotalDiscountsGiven = invoices.Sum(i => i.DiscountAmount),

                PendingCreditAmount = invoices
                    .Where(i => i.PaymentStatus == "Credit" || i.PaymentStatus == "Unpaid")
                    .Sum(i => i.FinalAmount),

                TotalInvoices = invoices.Count,

                TopSellingParts = topSellingParts.Select(item =>
                {
                    var part = parts.FirstOrDefault(p => p.Id == item.PartId);

                    return new TopSellingPartDto
                    {
                        PartId = item.PartId,
                        PartName = part?.Name ?? "Unknown Part",
                        TotalQuantitySold = item.TotalQuantitySold,TotalSalesAmount = item.TotalSalesAmount
                    };
                }).ToList()
            };
        }
    }
}