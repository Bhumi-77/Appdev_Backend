using Microsoft.EntityFrameworkCore;
using VehicleSystem.Application.DTOs;
using VehicleSystem.Application.Interfaces.Repositories;
using VehicleSystem.Infrastructure.Data;
using VehicleSystem.Domain.Models;

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

            var invoices = await _context.Invoices
                .Where(i => i.InvoiceDate >= startDate && i.InvoiceDate < endDate)
                .ToListAsync();

            return BuildReport("Daily", invoices);
        }

        public async Task<FinancialReportDto> GetMonthlyReportAsync(int year, int month)
        {
            var startDate = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
            var endDate = startDate.AddMonths(1);

            var invoices = await _context.Invoices
                .Where(i => i.InvoiceDate >= startDate && i.InvoiceDate < endDate)
                .ToListAsync();

            return BuildReport("Monthly", invoices);
        }

        public async Task<FinancialReportDto> GetYearlyReportAsync(int year)
        {
            var startDate = new DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var endDate = startDate.AddYears(1);

            var invoices = await _context.Invoices
                .Where(i => i.InvoiceDate >= startDate && i.InvoiceDate < endDate)
                .ToListAsync();

            return BuildReport("Yearly", invoices);
        }

        private FinancialReportDto BuildReport(string type, List<Invoice> invoices)
        {
            return new FinancialReportDto
            {
                ReportType = type,
                TotalSales = invoices.Sum(i => i.FinalAmount),
                TotalPaid = invoices.Where(i => i.PaymentStatus == "Paid").Sum(i => i.FinalAmount),
                TotalCredit = invoices.Where(i => i.PaymentStatus == "Credit").Sum(i => i.FinalAmount),
                TotalInvoices = invoices.Count
            };
        }
    }
}