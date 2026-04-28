using Microsoft.AspNetCore.Mvc;
using VehicleSystem.Application.Interfaces.Services;

namespace VehicleSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialReportsController : ControllerBase
    {
        private readonly IFinancialReportService _service;

        public FinancialReportsController(IFinancialReportService service)
        {
            _service = service;
        }

        [HttpGet("daily")]
        public async Task<IActionResult> GetDailyReport([FromQuery] DateTime date)
        {
            var result = await _service.GetDailyReportAsync(date);
            return Ok(result);
        }

        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyReport([FromQuery] int year, [FromQuery] int month)
        {
            var result = await _service.GetMonthlyReportAsync(year, month);
            return Ok(result);
        }

        [HttpGet("yearly")]
        public async Task<IActionResult> GetYearlyReport([FromQuery] int year)
        {
            var result = await _service.GetYearlyReportAsync(year);
            return Ok(result);
        }
    }
}