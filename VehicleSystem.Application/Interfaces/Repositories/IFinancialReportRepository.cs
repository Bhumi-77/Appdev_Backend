using VehicleSystem.Application.DTOs;

namespace VehicleSystem.Application.Interfaces.Repositories
{
    public interface IFinancialReportRepository
    {
        Task<FinancialReportDto> GetDailyReportAsync(DateTime date);
        Task<FinancialReportDto> GetMonthlyReportAsync(int year, int month);
        Task<FinancialReportDto> GetYearlyReportAsync(int year);
    }
}