using VehicleSystem.Application.DTOs;

namespace VehicleSystem.Application.Interfaces.Services
{
    public interface IFinancialReportService
    {
        Task<FinancialReportDto> GetDailyReportAsync(DateTime date);
        Task<FinancialReportDto> GetMonthlyReportAsync(int year, int month);
        Task<FinancialReportDto> GetYearlyReportAsync(int year);
    }
}