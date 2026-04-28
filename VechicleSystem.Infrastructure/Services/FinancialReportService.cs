using VehicleSystem.Application.DTOs;
using VehicleSystem.Application.Interfaces.Repositories;
using VehicleSystem.Application.Interfaces.Services;
using VehicleSystem.Infrastructure.Repositories;

namespace VehicleSystem.Infrastructure.Services
{
    public class FinancialReportService : IFinancialReportService
    {
        private readonly IFinancialReportRepository _repository;

        public FinancialReportService(IFinancialReportRepository repository)
        {
            _repository = repository;
        }

        public Task<FinancialReportDto> GetDailyReportAsync(DateTime date)
        {
            return _repository.GetDailyReportAsync(date);
        }

        public Task<FinancialReportDto> GetMonthlyReportAsync(int year, int month)
        {
            return _repository.GetMonthlyReportAsync(year, month);
        }

        public Task<FinancialReportDto> GetYearlyReportAsync(int year)
        {
            return _repository.GetYearlyReportAsync(year);
        }
    }
}